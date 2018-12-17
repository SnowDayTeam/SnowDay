// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno

using UnityEngine;
using UnityEditor;

// Manages the Gradient Textures created with the Ramp Generator

public class TCP2_GradientManager
{
	static public string LAST_SAVE_PATH
	{
		get { return EditorPrefs.GetString("TCP2_GradientSavePath", Application.dataPath); }
		set { EditorPrefs.SetString("TCP2_GradientSavePath", value); }
	}

	static public bool CreateAndSaveNewGradientTexture(int width, string unityPath)
	{
		var gradient = new Gradient();
		gradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.black, 0.45f), new GradientColorKey(Color.white, 0.55f) };
		gradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) };

		return SaveGradientTexture(gradient, width, unityPath);
	}

	static public bool SaveGradientTexture(Gradient gradient, int width, string unityPath)
	{
		Texture2D ramp = CreateGradientTexture(gradient, width);
		byte[] png = ramp.EncodeToPNG();
		Object.DestroyImmediate(ramp);

		string systemPath = Application.dataPath + "/" + unityPath.Substring(7);
		System.IO.File.WriteAllBytes(systemPath, png);

		AssetDatabase.ImportAsset(unityPath);
		TextureImporter ti = AssetImporter.GetAtPath(unityPath) as TextureImporter;
		ti.wrapMode = TextureWrapMode.Clamp;
		ti.isReadable = true;
#if UNITY_5_5_OR_NEWER
		ti.textureCompression = TextureImporterCompression.Uncompressed;
		ti.alphaSource = TextureImporterAlphaSource.None;
#else
		ti.textureFormat = TextureImporterFormat.RGB24;
#endif
		//Gradient data embedded in userData
		ti.userData = GradientToUserData(gradient);
		ti.SaveAndReimport();

		return true;
	}

	static public string GradientToUserData(Gradient gradient)
	{
		var output = "GRADIENT\n";
		for(int i = 0; i < gradient.colorKeys.Length; i++)
			output += ColorToHex(gradient.colorKeys[i].color) + "," + gradient.colorKeys[i].time + "#";
		output = output.TrimEnd('#');
		output += "\n";
		for(int i = 0; i < gradient.alphaKeys.Length; i++)
			output += gradient.alphaKeys[i].alpha + "," + gradient.alphaKeys[i].time + "#";
		output = output.TrimEnd('#');
#if UNITY_5_5_OR_NEWER
		output += "\n" + gradient.mode.ToString();
#endif

		return output;
	}

	static public void SetGradientFromUserData(string userData, Gradient gradient)
	{
		string[] keys = userData.Split('\n');
		if(keys == null || keys.Length < 3 || keys[0] != "GRADIENT")
		{
			EditorApplication.Beep();
			Debug.LogError("[TCP2_GradientManager] Invalid Gradient Texture\nMake sure the texture was created with the Ramp Generator.");
			return;
		}

		var ckData = keys[1].Split('#');
		var colorsKeys = new GradientColorKey[ckData.Length];
		for(int i = 0; i < ckData.Length; i++)
		{
			var data = ckData[i].Split(',');
			colorsKeys[i] = new GradientColorKey(HexToColor(data[0]), float.Parse(data[1]));
		}
		var akData = keys[2].Split('#');
		var alphaKeys = new GradientAlphaKey[akData.Length];
		for(int i = 0; i < akData.Length; i++)
		{
			var data = akData[i].Split(',');
			alphaKeys[i] = new GradientAlphaKey(float.Parse(data[0]), float.Parse(data[1]));
		}
		gradient.SetKeys(colorsKeys, alphaKeys);

#if UNITY_5_5_OR_NEWER
		if(keys.Length >= 4)
		{
			gradient.mode = (GradientMode)System.Enum.Parse(typeof(GradientMode), keys[3]);
		}
#endif
	}

	static private Texture2D CreateGradientTexture(Gradient gradient, int width)
	{
		Texture2D ramp = new Texture2D(width, 4, TextureFormat.RGB24, true, true);
		var colors = GetPixelsFromGradient(gradient, width);
		ramp.SetPixels(colors);
		ramp.Apply(true);
		return ramp;
	}

	static public Color[] GetPixelsFromGradient(Gradient gradient, int width)
	{
		var pixels = new Color[width*4];
		for(int x = 0; x < width; x++)
		{
			float delta = Mathf.Clamp01(x / (float)width);
			Color col = gradient.Evaluate(delta);
			pixels[x+0*width] = col;
			pixels[x+1*width] = col;
			pixels[x+2*width] = col;
			pixels[x+3*width] = col;
		}
		return pixels;
	}

	static public string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}

	static public Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
}
