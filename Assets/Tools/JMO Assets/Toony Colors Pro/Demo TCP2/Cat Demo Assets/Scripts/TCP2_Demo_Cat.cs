// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno

using UnityEngine;
using UnityEngine.UI;

public class TCP2_Demo_Cat : MonoBehaviour
{
	[System.Serializable]
	public class Ambience
	{
		public string name;
		public GameObject[] activate;
		public Material skybox;
	}

	[System.Serializable]
	public class ShaderStyle
	{
		[System.Serializable]
		public class CharacterSettings
		{
			public Material material;
			public Renderer[] renderers;
		}

		public string name;
		public CharacterSettings[] settings;
	}

	public Ambience[] ambiences;
	public int amb;
	[Space]
	public ShaderStyle[] styles;
	public int style;
	[Space]
	public GameObject shadedGroup;
	public GameObject flatGroup;
	[Space]
	public GameObject[] cats;
	public GameObject[] unityChans;
	public GameObject unityChanCopyright;
	[Space]
	public AnimationClip[] catAnimations;
	int catAnim;
	public AnimationClip[] unityChanAnimations;
	int uchanAnim;
	public bool animate { get; set; }
	float animTime;
	[Space]
	public Light[] dirLights;
	public Light[] otherLights;
	public Transform rotatingPointLights;
	public bool rotateLights { get; set; }
	public bool rotatePointLights { get; set; }
	[Space]
	public Button[] ambiencesButtons;
	public Button[] stylesButtons;
	public Button[] characterButtons;
	public Button[] textureButtons;
	public Button[] animationButtons;
	[Space]
	public Canvas canvas;

	//------------------------------------------------------------------------------------------------------------------------

	void Awake()
	{
		rotatePointLights = true;
		rotateLights = false;
		animate = true;
		SetAmbience(0);
		SetStyle(0);
		SetCat(true);
		SetFlat(false);
		SetAnimation(0);
	}

	void Update()
	{
		if(rotateLights)
			foreach(var l in dirLights)
				l.transform.Rotate(Vector3.up * Time.deltaTime * -30f, Space.World);

		if(rotatePointLights)
			rotatingPointLights.transform.Rotate(Vector3.up * 50f * Time.deltaTime, Space.World);

		if(animate)
		{
			animTime += Time.deltaTime;
			UpdateAnimation();
		}

		//Keyboard shortcuts
		//Switch style
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				SetStyle(--style);
			else
				SetStyle(++style);
		}

		//Show/hide UI
		if(Input.GetKeyDown(KeyCode.H))
		{
			canvas.enabled = !canvas.enabled;
		}
	}

	void UpdateAnimation()
	{
		foreach(var c in cats)
			if(c.activeInHierarchy)
				catAnimations[catAnim].SampleAnimation(c, animTime);

		foreach(var u in unityChans)
			if(u.activeInHierarchy)
				unityChanAnimations[uchanAnim].SampleAnimation(u, animTime);
	}

	//------------------------------------------------------------------------------------------------------------------------
	// UI Callbacks

	public void SetAmbience(int index)
	{
		foreach(var a in ambiences)
			foreach(var g in a.activate)
				g.SetActive(false);

		amb = index % ambiences.Length;
		var current = ambiences[amb];
		foreach(var g in current.activate)
			g.SetActive(true);

		RenderSettings.skybox = current.skybox;
		DynamicGI.UpdateEnvironment();

		for(int i = 0; i < ambiencesButtons.Length; i++)
		{
			var colors = ambiencesButtons[i].colors;
			colors.colorMultiplier = (i == index) ? 0.96f : 0.6f;
			ambiencesButtons[i].colors = colors;
		}
	}

	public void SetStyle(int index)
	{
		if(index < 0)
			index = styles.Length-1;
		if(index >= styles.Length)
			index = 0;
		style = index;

		var s = styles[style];

		foreach(var setting in s.settings)
			foreach(var r in setting.renderers)
				r.sharedMaterial = setting.material;

		for(int i = 0; i < stylesButtons.Length; i++)
		{
			var colors = stylesButtons[i].colors;
			colors.colorMultiplier = (i == index) ? 0.96f : 0.6f;
			stylesButtons[i].colors = colors;
		}
	}

	public void SetFlat(bool flat)
	{
		shadedGroup.SetActive(!flat);
		flatGroup.SetActive(flat);
		UpdateAnimation();

		for(int i = 0; i < textureButtons.Length; i++)
		{
			var colors = textureButtons[i].colors;
			colors.colorMultiplier = (i == 1 && flat) || (i == 0 && !flat) ? 0.96f : 0.6f;
			textureButtons[i].colors = colors;
		}
	}

	public void SetCat(bool cat)
	{
		foreach(var c in cats)
			c.SetActive(cat);
		foreach(var u in unityChans)
			u.SetActive(!cat);
		UpdateAnimation();

		unityChanCopyright.SetActive(!cat);

		for(int i = 0; i < characterButtons.Length; i++)
		{
			var colors = characterButtons[i].colors;
			colors.colorMultiplier = (i == 0 && cat) || (i == 1 && !cat) ? 0.96f : 0.6f;
			characterButtons[i].colors = colors;
		}
	}

	public void SetLightShadows(bool on)
	{
		foreach(var l in dirLights)
			l.shadows = on ? LightShadows.Soft : LightShadows.None;

		foreach(var l in otherLights)
			l.shadows = on ? LightShadows.Soft : LightShadows.None;
	}

	public void SetAnimation(int index)
	{
		catAnim = index;
		if(catAnim >= catAnimations.Length)
			catAnim = 0;
		if(catAnim < 0)
			catAnim = catAnimations.Length-1;

		uchanAnim = index;
		if(uchanAnim >= unityChanAnimations.Length)
			uchanAnim = 0;
		if(uchanAnim < 0)
			uchanAnim = unityChanAnimations.Length-1;

		for(int i = 0; i < animationButtons.Length; i++)
		{
			var colors = animationButtons[i].colors;
			colors.colorMultiplier = (i == index) ? 0.96f : 0.6f;
			animationButtons[i].colors = colors;
		}
	}
}
