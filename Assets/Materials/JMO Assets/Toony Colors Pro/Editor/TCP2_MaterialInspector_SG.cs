// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno

//Enable this to display the default Inspector (in case the custom Inspector is broken)
//#define SHOW_DEFAULT_INSPECTOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// Custom material inspector for generated shader

public class TCP2_MaterialInspector_SG : ShaderGUI
{
	//Properties
	private Material targetMaterial { get { return (mMaterialEditor == null) ? null : mMaterialEditor.target as Material; } }
	private MaterialEditor mMaterialEditor;
	private Stack<bool> toggledGroups = new Stack<bool>();

	//--------------------------------------------------------------------------------------------------

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		mMaterialEditor = materialEditor;

#if SHOW_DEFAULT_INSPECTOR
		base.OnGUI();
		return;
#else

		//Header
		EditorGUILayout.BeginHorizontal();
		string label = (Screen.width > 450f) ? "TOONY COLORS PRO 2 - INSPECTOR (Generated Shader)" : (Screen.width > 300f ? "TOONY COLORS PRO 2 - INSPECTOR" : "TOONY COLORS PRO 2");
		TCP2_GUI.HeaderBig(label);
		if(TCP2_GUI.Button(TCP2_GUI.CogIcon, "O", "Open in Shader Generator"))
		{
			if(targetMaterial.shader != null)
			{
				TCP2_ShaderGenerator.OpenWithShader(targetMaterial.shader);
			}
		}
		EditorGUILayout.EndHorizontal();
		TCP2_GUI.Separator();

		//Iterate Shader properties
		materialEditor.serializedObject.Update();
		SerializedProperty mShader = materialEditor.serializedObject.FindProperty("m_Shader");
		toggledGroups.Clear();
		if(materialEditor.isVisible && !mShader.hasMultipleDifferentValues && mShader.objectReferenceValue != null)
		{
			//Retina display fix
			EditorGUIUtility.labelWidth = TCP2_Utils.ScreenWidthRetina - 120f;
			EditorGUIUtility.fieldWidth = 64f;

			EditorGUI.BeginChangeCheck();

			EditorGUI.indentLevel++;
			foreach (MaterialProperty p in properties)
			{
				bool visible = (toggledGroups.Count == 0 || toggledGroups.Peek());

				//Hacky way to separate material inspector properties into foldout groups
				if(p.name.StartsWith("__BeginGroup"))
				{
					//Foldout
					if(visible)
					{
						GUILayout.Space(8f);
						p.floatValue = EditorGUILayout.Foldout(p.floatValue > 0, p.displayName, TCP2_GUI.FoldoutBold) ? 1 : 0;
					}

					EditorGUI.indentLevel++;
					toggledGroups.Push((p.floatValue > 0) && visible);
				}
				else if(p.name.StartsWith("__EndGroup"))
				{
					EditorGUI.indentLevel--;
					toggledGroups.Pop();
					GUILayout.Space(8f);
				}
				else
				{
					//Draw regular property
					if(visible && (p.flags & (MaterialProperty.PropFlags.PerRendererData | MaterialProperty.PropFlags.HideInInspector)) == MaterialProperty.PropFlags.None)
						mMaterialEditor.ShaderProperty(p, p.displayName);
				}
			}
			EditorGUI.indentLevel--;

			if (EditorGUI.EndChangeCheck())
			{
				materialEditor.PropertiesChanged();
			}
		}

#endif     // !SHOW_DEFAULT_INSPECTOR

#if UNITY_5_5_OR_NEWER
		TCP2_GUI.Separator();
		materialEditor.RenderQueueField();
#endif
#if UNITY_5_6_OR_NEWER
		materialEditor.EnableInstancingField();
#endif
	}

	//--------------------------------------------------------------------------------------------------
	// Properties GUI

	private void DirectionalAmbientGUI(string filter, MaterialProperty[] properties)
	{
		float width = (EditorGUIUtility.currentViewWidth-20)/6;
		EditorGUILayout.BeginHorizontal();
		foreach(MaterialProperty p in properties)
		{
			//Filter
			string displayName = p.displayName;
			if(filter != null)
			{
				if(!displayName.Contains(filter))
					continue;
				displayName = displayName.Remove(displayName.IndexOf(filter), filter.Length+1);
			}
			else if(displayName.Contains("#"))
				continue;

			GUILayout.Label(displayName, GUILayout.Width(width));
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		foreach(MaterialProperty p in properties)
		{
			//Filter
			string displayName = p.displayName;
			if(filter != null)
			{
				if(!displayName.Contains(filter))
					continue;
				displayName = displayName.Remove(displayName.IndexOf(filter), filter.Length+1);
			}
			else if(displayName.Contains("#"))
				continue;
			
			DirAmbientColorProperty(p, displayName, width);
		}
		EditorGUILayout.EndHorizontal();
	}

	private Color DirAmbientColorProperty(MaterialProperty prop, string label, float width)
	{
		EditorGUI.BeginChangeCheck();
		EditorGUI.showMixedValue = prop.hasMixedValue;
		Color colorValue = EditorGUILayout.ColorField(prop.colorValue, GUILayout.Width(width));
		EditorGUI.showMixedValue = false;
		if(EditorGUI.EndChangeCheck())
		{
			prop.colorValue = colorValue;
		}
		return prop.colorValue;
	}
}
