// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno

// This script handles lights' and ambient intensities depending on if the Editor is set to Gamma or Linear color space

using UnityEngine;

[ExecuteInEditMode]
public class TCP2_Demo_GammaLinear : MonoBehaviour
{
	[System.Serializable]
	public class LightSettings
	{
		public Light light;
		public float gammaIntensity;
		public float linearIntensity;
	}

	[System.Serializable]
	public class MaterialSettings
	{
		public Material material;
		public Color gammaColor;
		public Color linearColor;
	}

	public LightSettings[] lights;
	public MaterialSettings[] materials;
	ColorSpace lastColorSpace;

#if UNITY_EDITOR
	void Awake()
	{
		lastColorSpace = QualitySettings.activeColorSpace;
		UpdateLighting();
	}

	void Update()
	{
		if(lastColorSpace != QualitySettings.activeColorSpace)
		{
			lastColorSpace = QualitySettings.activeColorSpace;
			UpdateLighting();
		}
	}

	void UpdateLighting()
	{
		bool isLinear = (QualitySettings.activeColorSpace == ColorSpace.Linear);

		if(lights != null)
		{
			foreach(var ls in lights)
			{
				ls.light.intensity = isLinear ? ls.linearIntensity : ls.gammaIntensity;
			}
		}

		if(lights != null)
		{
			foreach(var mat in materials)
			{
				mat.material.color = isLinear ? mat.linearColor : mat.gammaColor;
			}
		}
	}
#endif
}
