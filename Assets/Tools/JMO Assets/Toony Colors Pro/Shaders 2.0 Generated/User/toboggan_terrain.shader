// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno


Shader "Toony Colors Pro 2/User/toboggan_terrain"
{
	Properties
	{
		//TOONY COLORS
		_HColor ("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
		_SColor ("Shadow Color", Color) = (0.3,0.3,0.3,1.0)

		//TOONY COLORS RAMP
	[TCP2Header(RAMP SETTINGS)]
		[TCP2Gradient] _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}

	[TCP2Separator]

		//TERRAIN PROPERTIES
		[HideInInspector] _Control ("Control (RGBA)", 2D) = "red" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {}
		// used in fallback on old cards & base map
		[HideInInspector] _MainTex ("BaseMap (RGB)", 2D) = "white" {}
		[HideInInspector] _Color ("Main Color", Color) = (1,1,1,1)
	}

	CGINCLUDE
		#pragma surface surf ToonyColorsCustom  vertex:SplatmapVert_TCP2 finalcolor:SplatmapFinalColor_TCP2

		#pragma multi_compile_fog
		#pragma multi_compile TCP2_RAMPTEXT

		sampler2D _Control;
		float4 _Control_ST;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
	#ifdef _TERRAIN_NORMAL_MAP
		sampler2D _Normal0, _Normal1, _Normal2, _Normal3;
	#endif


		struct Input
		{
			//TERRAIN

			float2 uv_Splat0 : TEXCOORD0;
			float2 uv_Splat1 : TEXCOORD1;
			float2 uv_Splat2 : TEXCOORD2;
			float2 uv_Splat3 : TEXCOORD3;
			float2 tc_Control : TEXCOORD4;	// Not prefixing '_Contorl' with 'uv' allows a tighter packing of interpolators, which is necessary to support directional lightmap.
			UNITY_FOG_COORDS(5)

		};

		struct appdata_tcp2
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
	#ifdef _TERRAIN_NORMAL_MAP
			float4 tangent : TANGENT;
	#endif
	#if UNITY_VERSION >= 550
			UNITY_VERTEX_INPUT_INSTANCE_ID
	#endif
		};

		void SplatmapVert_TCP2(inout appdata_tcp2 v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

			//------------------
			//TERRAIN
			o.tc_Control = TRANSFORM_TEX(v.texcoord, _Control);	// Need to manually transform uv here, as we choose not to use 'uv' prefix for this texcoord.
			float4 pos = UnityObjectToClipPos(v.vertex);
			UNITY_TRANSFER_FOG(o, pos);		

		#ifdef _TERRAIN_NORMAL_MAP
			v.tangent.xyz = cross(v.normal, float3(0,0,1));
			v.tangent.w = -1;
		#endif
			//------------------

		}

		void SplatmapMix(Input IN, out half4 splat_control, out half weight, out fixed4 mixedDiffuse, inout fixed3 mixedNormal)
		{
			splat_control = tex2D(_Control, IN.tc_Control);
			weight = dot(splat_control, half4(1,1,1,1));

		#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)
				clip(weight - 0.0039 /*1/255*/);
		#endif

			// Normalize weights before lighting and restore weights in final modifier functions so that the overal
			// lighting result can be correctly weighted.
			splat_control /= (weight + 1e-3f);

			//Sample all 4 terrain textures
			fixed4 tex1 = tex2D(_Splat0, IN.uv_Splat0);
			fixed4 tex2 = tex2D(_Splat1, IN.uv_Splat1);
			fixed4 tex3 = tex2D(_Splat2, IN.uv_Splat2);
			fixed4 tex4 = tex2D(_Splat3, IN.uv_Splat3);

			mixedDiffuse = 0.0f;

			mixedDiffuse += splat_control.r * tex1;
			mixedDiffuse += splat_control.g * tex2;
			mixedDiffuse += splat_control.b * tex3;
			mixedDiffuse += splat_control.a * tex4;

	#ifdef _TERRAIN_NORMAL_MAP

			//Sample all 4 terrain normal maps
			fixed4 bump1 = tex2D(_Normal0, IN.uv_Splat0);
			fixed4 bump2 = tex2D(_Normal1, IN.uv_Splat1);
			fixed4 bump3 = tex2D(_Normal2, IN.uv_Splat2);
			fixed4 bump4 = tex2D(_Normal3, IN.uv_Splat3);
			fixed4 nrm = 0.0f;
			nrm += splat_control.r * bump1;
			nrm += splat_control.g * bump2;
			nrm += splat_control.b * bump3;
			nrm += splat_control.a * bump4;
			mixedNormal = UnpackNormal(nrm);
	#endif
		}
		#ifndef TERRAIN_SURFACE_OUTPUT
			#define TERRAIN_SURFACE_OUTPUT SurfaceOutputCustom
		#endif

		//Custom SurfaceOutput
		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
		};

		void SplatmapFinalColor_TCP2(Input IN, TERRAIN_SURFACE_OUTPUT o, inout fixed4 color)
		{
			color *= o.Alpha;
			#ifdef TERRAIN_SPLAT_ADDPASS
				UNITY_APPLY_FOG_COLOR(IN.fogCoord, color, fixed4(0,0,0,0));
			#else
				UNITY_APPLY_FOG(IN.fogCoord, color);
			#endif

		}

		//================================================================
		// CUSTOM LIGHTING

		//Lighting-related variables
		fixed4 _HColor;
		fixed4 _SColor;
		sampler2D _Ramp;

		inline half4 LightingToonyColorsCustom (inout TERRAIN_SURFACE_OUTPUT s, half3 lightDir, half3 viewDir, half atten)
		{
			s.Normal = normalize(s.Normal);
			fixed ndl = max(0, dot(s.Normal, lightDir));

			fixed3 ramp = tex2D(_Ramp, fixed2(ndl,ndl));
		#if !(POINT) && !(SPOT)
			ramp *= atten;
		#endif

			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
			ramp = lerp(_SColor.rgb, _HColor.rgb, ramp);
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp;
			c.a = s.Alpha;
		#if (POINT || SPOT)
			c.rgb *= atten;
		#endif
			return c;
		}


		//================================================================

		void surf(Input IN, inout TERRAIN_SURFACE_OUTPUT o)
		{

			//TERRAIN
			half4 splat_control;
			half weight;
			fixed4 mixedDiffuse;
			SplatmapMix(IN, splat_control, weight, mixedDiffuse, o.Normal);
			o.Albedo = mixedDiffuse.rgb;
			o.Alpha = weight;

		}
	ENDCG

	Category
	{
		Tags
		{
			"Queue" = "Geometry-99"
			"RenderType" = "Opaque"
		}

		// TODO: Seems like "#pragma target 3.0 _TERRAIN_NORMAL_MAP" can't fallback correctly on less capable devices?
		// Use two sub-shaders to simulate different features for different targets and still fallback correctly.
		// SM3+ targets
		SubShader
		{
			CGPROGRAM
				#pragma target 3.0
				#pragma multi_compile __ _TERRAIN_NORMAL_MAP
			ENDCG
		}

	}

	Dependency "AddPassShader" = "Hidden/Toony Colors Pro 2/User/toboggan_terrain-AddPass"
	Dependency "BaseMapShader" = "Hidden/Toony Colors Pro 2/User/toboggan_terrain-Base"
	Dependency "Details0"      = "Hidden/TerrainEngine/Details/Vertexlit"
	Dependency "Details1"      = "Hidden/TerrainEngine/Details/WavingDoublePass"
	Dependency "Details2"      = "Hidden/TerrainEngine/Details/BillboardWavingDoublePass"
	Dependency "Tree0"         = "Hidden/TerrainEngine/BillboardTree"

	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector_SG"
}

