// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// Graphical User Interface helper functions

public static class TCP2_GUI
{
	static private GUIStyle _EnabledLabel;
	static private GUIStyle EnabledLabel
	{
		get
		{
			if(_EnabledLabel == null)
			{
				_EnabledLabel = new GUIStyle(EditorStyles.label);
				_EnabledLabel.normal.background = EnabledBg;
			}
			return _EnabledLabel;
		}
	}

	static private GUIStyle _HelpIcon;
	static public GUIStyle HelpIcon
	{
		get
		{
			if(_HelpIcon == null)
			{
				_HelpIcon = new GUIStyle(EditorStyles.label);
				_HelpIcon.fixedWidth = 16;
				_HelpIcon.fixedHeight = 16;

				string rootPath = TCP2_Utils.FindReadmePath(true);
				Texture2D icon = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_HelpIcon.png", typeof(Texture2D)) as Texture2D;
				Texture2D icon_down = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_HelpIcon_Down.png", typeof(Texture2D)) as Texture2D;
				if(icon == null || icon_down == null) return null;

				_HelpIcon.normal.background = icon;
				_HelpIcon.active.background = icon_down;
			}

			return _HelpIcon;
		}
	}

	static private Texture2D _EnabledBg;
	static private Texture2D _EnabledBgPro;
	static public Texture2D EnabledBg
	{
		get
		{
			if(_EnabledBg == null)
			{
				string rootPath = TCP2_Utils.FindReadmePath(true);
				_EnabledBg = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_EnabledBg.png", typeof(Texture2D)) as Texture2D;
				_EnabledBgPro = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_EnabledBgPro.png", typeof(Texture2D)) as Texture2D;
			}
			return (EditorGUIUtility.isProSkin && _EnabledBgPro != null) ? _EnabledBgPro : _EnabledBg;
		}
	}

	static private GUIStyle _CogIcon;
	static public GUIStyle CogIcon
	{
		get
		{
			if(_CogIcon == null)
			{
				_CogIcon = new GUIStyle(EditorStyles.label);
				_CogIcon.fixedWidth = 16;
				_CogIcon.fixedHeight = 16;

				string rootPath = TCP2_Utils.FindReadmePath(true);
				Texture2D icon = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_CogIcon.png", typeof(Texture2D)) as Texture2D;
				Texture2D icon_down = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/TCP2_CogIcon_Down.png", typeof(Texture2D)) as Texture2D;
				if(icon == null || icon_down == null) return null;

				_CogIcon.normal.background = icon;
				_CogIcon.active.background = icon_down;
			}

			return _CogIcon;
		}
	}

	static private GUIStyle _HeaderLabel;
	static private GUIStyle _HeaderLabelPro;
	static private GUIStyle HeaderLabel
	{
		get
		{
			if(_HeaderLabel == null)
			{
				_HeaderLabel = new GUIStyle(EditorStyles.label);
				_HeaderLabel.fontStyle = FontStyle.Bold;
				_HeaderLabel.normal.textColor = new Color(0.35f, 0.35f, 0.35f);

				_HeaderLabelPro = new GUIStyle(_HeaderLabel);
				_HeaderLabelPro.normal.textColor = new Color(0.7f, 0.7f, 0.7f);
			}
			return EditorGUIUtility.isProSkin ? _HeaderLabelPro : _HeaderLabel;
		}
	}

	static private GUIStyle _HeaderDropDown;
	static private GUIStyle _HeaderDropDownPro;
	static private GUIStyle HeaderDropDown
	{
		get
		{
			if(_HeaderDropDown == null)
			{
				_HeaderDropDown = new GUIStyle(EditorStyles.foldout);
				_HeaderDropDown.fontStyle = FontStyle.Bold;

				var textColor = new Color(0.35f, 0.35f, 0.35f);
				_HeaderDropDown.normal.textColor = textColor;
				_HeaderDropDown.onNormal.textColor = textColor;

				var textColorPro = new Color(0.6f, 0.6f, 0.6f);
				_HeaderDropDownPro = new GUIStyle(_HeaderDropDown);
				_HeaderDropDownPro.normal.textColor = textColorPro;
				_HeaderDropDownPro.onNormal.textColor = textColorPro;
			}
			return EditorGUIUtility.isProSkin ? _HeaderDropDownPro : _HeaderDropDown;
		}
	}

	static private GUIStyle _SubHeaderLabel;
	static private GUIStyle _SubHeaderLabelPro;
	static private GUIStyle SubHeaderLabel
	{
		get
		{
			if(_SubHeaderLabel == null)
			{
				_SubHeaderLabel = new GUIStyle(EditorStyles.label);
				_SubHeaderLabel.fontStyle = FontStyle.Normal;
				_SubHeaderLabel.normal.textColor = new Color(0.35f, 0.35f, 0.35f);

				_SubHeaderLabelPro = new GUIStyle(_SubHeaderLabel);
				_SubHeaderLabelPro.normal.textColor = new Color(0.7f, 0.7f, 0.7f);
			}
			return EditorGUIUtility.isProSkin ? _SubHeaderLabelPro : _SubHeaderLabel;
		}
	}

	static private GUIStyle _BigHeaderLabel;
	static private GUIStyle BigHeaderLabel
	{
		get
		{
			if(_BigHeaderLabel == null)
			{
				_BigHeaderLabel = new GUIStyle(EditorStyles.largeLabel);
				_BigHeaderLabel.fontStyle = FontStyle.Bold;
				_BigHeaderLabel.fixedHeight = 30;
			}
			return _BigHeaderLabel;
		}
	}

	static private GUIStyle _FoldoutBold;
	static public GUIStyle FoldoutBold
	{
		get
		{
			if(_FoldoutBold == null)
			{
				_FoldoutBold = new GUIStyle(EditorStyles.foldout);
				_FoldoutBold.fontStyle = FontStyle.Bold;
			}
			return _FoldoutBold;
		}
	}

	static public GUIStyle _LineStyle;
	static public GUIStyle LineStyle
	{
		get
		{
			if(_LineStyle == null)
			{
				_LineStyle = new GUIStyle();
				_LineStyle.normal.background = EditorGUIUtility.whiteTexture;
				_LineStyle.stretchWidth = true;
			}

			return _LineStyle;
		}
	}

	static GUIStyle _HelpBoxRichTextStyle;
	static public GUIStyle HelpBoxRichTextStyle
	{
		get
		{
			if(_HelpBoxRichTextStyle == null)
			{
				_HelpBoxRichTextStyle = new GUIStyle("HelpBox");
				_HelpBoxRichTextStyle.richText = true;
			}
			return _HelpBoxRichTextStyle;
		}
	}

	static Texture2D warningIconCached;
	static Texture2D infoIconCached;
	static Texture2D errorIconCached;
	static public Texture2D GetHelpBoxIcon(MessageType msgType)
	{
		string iconName = null;
		switch(msgType)
		{
			case MessageType.Error:
				if(errorIconCached != null)
					return errorIconCached;
				iconName = "TCP2_ErrorIcon";
				break;
			case MessageType.Warning:
				if(warningIconCached != null)
					return warningIconCached;
				iconName = "TCP2_WarningIcon";
				break;
			case MessageType.Info:
				if(infoIconCached != null)
					return infoIconCached;
				iconName = "TCP2_InfoIcon";
				break;
		}

		if(string.IsNullOrEmpty(iconName))
			return null;

		string rootPath = TCP2_Utils.FindReadmePath(true);
		Texture2D icon = AssetDatabase.LoadAssetAtPath("Assets" + rootPath + "/Editor/Icons/" + iconName + ".png", typeof(Texture2D)) as Texture2D;

		switch(msgType)
		{
			case MessageType.Error: errorIconCached = icon; break;
			case MessageType.Warning: warningIconCached = icon; break;
			case MessageType.Info: infoIconCached = icon; break;
		}

		return icon;
	}

	//--------------------------------------------------------------------------------------------------
	// HELP

	static public void HelpButton(Rect rect, string helpTopic, string helpAnchor = null)
	{
		if(TCP2_GUI.Button(rect, HelpIcon, "?", "Help about:\n" + helpTopic))
		{
			OpenHelpFor(string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor);
		}
	}
	static public void HelpButton(string helpTopic, string helpAnchor = null)
	{
		if(TCP2_GUI.Button(HelpIcon, "?", "Help about:\n" + helpTopic))
		{
			OpenHelpFor(string.IsNullOrEmpty(helpAnchor) ? helpTopic : helpAnchor);
		}
	}

	static public void OpenHelpFor(string helpTopic)
	{
		string rootDir = TCP2_Utils.FindReadmePath();
		if(rootDir == null)
		{
			EditorUtility.DisplayDialog("TCP2 Documentation", "Couldn't find TCP2 root folder! (the readme file is missing)\nYou can still access the documentation manually in the Documentation folder.", "Ok");
		}
		else
		{
			string helpAnchor = helpTopic.Replace("/", "_").Replace(@"\", "_").Replace(" ", "_").ToLowerInvariant() + ".htm";
			string topicLink = "file:///" + rootDir.Replace(@"\", "/") + "/Documentation/Documentation Data/Anchors/" + helpAnchor;
			Application.OpenURL(topicLink);
		}
	}

	static public void OpenHelp()
	{
		string rootDir = TCP2_Utils.FindReadmePath();
		if(rootDir == null)
		{
			EditorUtility.DisplayDialog("TCP2 Documentation", "Couldn't find TCP2 root folder! (the readme file is missing)\nYou can still access the documentation manually in the Documentation folder.", "Ok");
		}
		else
		{
			string helpLink = "file:///" + rootDir.Replace(@"\", "/") + "/Documentation/TCP2 Documentation.html";
			Application.OpenURL(helpLink);
		}
	}

	//--------------------------------------------------------------------------------------------------
	//GUI Functions

	static public void Separator()
	{
		var colorDark = EditorGUIUtility.isProSkin ? new Color(.1f, .1f, .1f) : new Color(.3f, .3f, .3f);
		var colorBright = EditorGUIUtility.isProSkin ? new Color(.3f, .3f, .3f) : new Color(.9f, .9f, .9f);

		GUILayout.Space(4);
		GUILine(colorDark, 1);
		GUILine(colorBright, 1);
		GUILayout.Space(4);
	}

	static public void Separator(Rect position)
	{
		Rect lineRect = position;
		lineRect.height = 1;
		GUILine(lineRect, new Color(.3f, .3f, .3f), 1);
		lineRect.y += 1;
		GUILine(lineRect, new Color(.9f, .9f, .9f), 1);
	}

	static public void SeparatorBig()
	{
		GUILayout.Space(10);
		GUILine(new Color(.3f, .3f, .3f), 2);
		GUILayout.Space(1);
		GUILine(new Color(.3f, .3f, .3f), 2);
		GUILine(new Color(.85f, .85f, .85f), 1);
		GUILayout.Space(2);
	}

	static public void GUILine(float height = 2f)
	{
		GUILine(Color.black, height);
	}
	static public void GUILine(Color color, float height = 2f)
	{
		Rect position = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height, LineStyle);

		if(Event.current.type == EventType.Repaint)
		{
			Color orgColor = GUI.color;
			GUI.color = orgColor * color;
			LineStyle.Draw(position, false, false, false, false);
			GUI.color = orgColor;
		}
	}
	static public void GUILine(Rect position, Color color, float height = 2f)
	{
		if(Event.current.type == EventType.Repaint)
		{
			Color orgColor = GUI.color;
			GUI.color = orgColor * color;
			LineStyle.Draw(position, false, false, false, false);
			GUI.color = orgColor;
		}
	}

	//----------------------

	static public void Header(string header, string tooltip = null, bool expandWidth = false)
	{
		if(tooltip != null)
			EditorGUILayout.LabelField(new GUIContent(header, tooltip), HeaderLabel, GUILayout.ExpandWidth(expandWidth));
		else
			EditorGUILayout.LabelField(header, HeaderLabel, GUILayout.ExpandWidth(expandWidth));
	}

	static public void Header(Rect position, string header, string tooltip = null, bool expandWidth = false)
	{
		if(tooltip != null)
			EditorGUI.LabelField(position, new GUIContent(header, tooltip), HeaderLabel);
		else
			EditorGUI.LabelField(position, header, HeaderLabel);
	}

	static public bool HeaderFoldout(bool foldout, GUIContent guiContent)
	{
		foldout = EditorGUILayout.Foldout(foldout, guiContent, true, HeaderDropDown);
		return foldout;
	}

	static public void SubHeaderGray(string header, string tooltip = null, bool expandWidth = false)
	{
		if(tooltip != null)
			EditorGUILayout.LabelField(new GUIContent(header, tooltip), SubHeaderLabel, GUILayout.ExpandWidth(expandWidth));
		else
			EditorGUILayout.LabelField(header, SubHeaderLabel, GUILayout.ExpandWidth(expandWidth));
	}

	static public void HeaderAndHelp(string header, string helpTopic)
	{
		HeaderAndHelp(header, null, helpTopic);
	}
	static public void HeaderAndHelp(string header, string tooltip, string helpTopic)
	{
		GUILayout.BeginHorizontal();
		Rect r = GUILayoutUtility.GetRect(new GUIContent(header, tooltip), EditorStyles.label, GUILayout.ExpandWidth(true));
		Rect btnRect = r;
		btnRect.width = 16;
		//Button
		if(GUI.Button(btnRect, new GUIContent("", "Help about:\n" + helpTopic), HelpIcon))
			OpenHelpFor(helpTopic);
		//Label
		r.x += 16;
		r.width -= 16;
		GUI.Label(r, new GUIContent(header, tooltip), EditorStyles.boldLabel);
		GUILayout.EndHorizontal();
	}
	static public void HeaderAndHelp(Rect position, string header, string tooltip, string helpTopic)
	{
		if(!string.IsNullOrEmpty(helpTopic))
		{
			Rect btnRect = position;
			btnRect.width = 16;
			//Button
			if(GUI.Button(btnRect, new GUIContent("", "Help about:\n" + helpTopic), HelpIcon))
				OpenHelpFor(helpTopic);
		}

		//Label
		position.x += 16;
		position.width -= 16;
		GUI.Label(position, new GUIContent(header, tooltip), EditorStyles.boldLabel);
	}

	static public void HeaderBig(string header, string tooltip = null)
	{
		if(tooltip != null)
			EditorGUILayout.LabelField(new GUIContent(header, tooltip), BigHeaderLabel);
		else
			EditorGUILayout.LabelField(header, BigHeaderLabel);
	}

	static public void SubHeader(string header, string tooltip = null, float width = 146f)
	{
		SubHeader(header, tooltip, false, width);
	}
	static public void SubHeader(string header, string tooltip, bool highlight, float width)
	{
		if(tooltip != null)
			GUILayout.Label(new GUIContent(header, tooltip), highlight ? EnabledLabel : EditorStyles.label, GUILayout.Width(width));
		else
			GUILayout.Label(header, highlight ? EnabledLabel : EditorStyles.label, GUILayout.Width(width));
	}

	static public void SubHeader(Rect position, string header, string tooltip, bool highlight)
	{
		SubHeader(position, new GUIContent(header, tooltip), highlight);
	}
	static public void SubHeader(Rect position, GUIContent content, bool highlight)
	{
		GUI.Label(position, content, highlight ? EnabledLabel : EditorStyles.label);
	}

	static public void HelpBox(Rect position, string message, MessageType msgType)
	{
		EditorGUI.LabelField(position, GUIContent.none, new GUIContent(message, TCP2_GUI.GetHelpBoxIcon(msgType)), TCP2_GUI.HelpBoxRichTextStyle);
	}

	static public void HelpBoxLayout(string message, MessageType msgType)
	{
		EditorGUILayout.LabelField(GUIContent.none, new GUIContent(message, TCP2_GUI.GetHelpBoxIcon(msgType)), TCP2_GUI.HelpBoxRichTextStyle);
	}


	//----------------------

	static public bool Button(GUIStyle icon, string noIconText, string tooltip = null)
	{
		if(icon == null)
			return GUILayout.Button(new GUIContent(noIconText, tooltip), EditorStyles.miniButton);
		else
			return GUILayout.Button(new GUIContent("", tooltip), icon);
	}

	static public bool Button(Rect rect, GUIStyle icon, string noIconText, string tooltip = null)
	{
		if(icon == null)
			return GUI.Button(rect, new GUIContent(noIconText, tooltip), EditorStyles.miniButton);
		else
			return GUI.Button(rect, new GUIContent("", tooltip), icon);
	}

	static public int RadioChoice(int choice, bool horizontal, params string[] labels)
	{
		var guiContents = new GUIContent[labels.Length];
		for(int i = 0; i < guiContents.Length; i++)
		{
			guiContents[i] = new GUIContent(labels[i]);
		}
		return RadioChoice(choice, horizontal, guiContents);
	}
	static public int RadioChoice(int choice, bool horizontal, params GUIContent[] labels)
	{
		if(horizontal)
			EditorGUILayout.BeginHorizontal();

		for(int i = 0; i < labels.Length; i++)
		{
			GUIStyle style = EditorStyles.miniButton;
			if(labels.Length > 1)
			{
				if(i == 0)
					style = EditorStyles.miniButtonLeft;
				else if(i == labels.Length-1)
					style = EditorStyles.miniButtonRight;
				else
					style = EditorStyles.miniButtonMid;
			}

			if(GUILayout.Toggle(i == choice, labels[i], style))
			{
				choice = i;
			}
		}

		if(horizontal)
			EditorGUILayout.EndHorizontal();

		return choice;
	}

	static public int RadioChoiceHorizontal(Rect position, int choice, params GUIContent[] labels)
	{
		for(int i = 0; i < labels.Length; i++)
		{
			Rect rI = position;
			rI.width /= labels.Length;
			rI.x += i * rI.width;
			if(GUI.Toggle(rI, choice == i, labels[i], (i == 0) ? EditorStyles.miniButtonLeft : (i == labels.Length - 1) ? EditorStyles.miniButtonRight : EditorStyles.miniButtonMid))
			{
				choice = i;
			}
		}

		return choice;
	}
}

//===================================================================================================================================================================
// Material Property Drawers

public class TCP2HeaderHelpDecorator : MaterialPropertyDrawer
{
	protected readonly string header;
	protected readonly string help;

	public TCP2HeaderHelpDecorator(string header)
	{
		this.header = header;
		this.help = null;
	}
	public TCP2HeaderHelpDecorator(string header, string help)
	{
		this.header = header;
		this.help = help;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		TCP2_GUI.HeaderAndHelp(position, this.header, null, this.help);
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return 18f;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2HelpBoxDecorator : MaterialPropertyDrawer
{
	protected readonly MessageType msgType;
	protected readonly string message;
	protected Texture2D icon;

	private float InspectorWidth = 0;   //Workaround to detect vertical scrollbar in the inspector
	static string ParseMessage(string message)
	{
		//double space = line break
		message = message.Replace("  ", "\n");

		// __word__ = <b>word</b>
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		string[] words = message.Split(' ');
		for(int i = 0; i < words.Length; i++)
		{
			var w = words[i];
			if(w.StartsWith("__") && w.EndsWith("__"))
			{
				var w2 = w.Replace("__", "");
				w = w.Replace("__" + w2 + "__", "<b>" + w2 + "</b>");
			}

			sb.Append(w + " ");
		}
		var str = sb.ToString();
		return str.TrimEnd();
	}

	public TCP2HelpBoxDecorator(string messageType, string msg)
	{
		this.msgType = (MessageType)System.Enum.Parse(typeof(MessageType), messageType);
		this.message = ParseMessage(msg);
		this.icon = TCP2_GUI.GetHelpBoxIcon(this.msgType);
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		position.height -= 4f;
		EditorGUI.LabelField(position, GUIContent.none, new GUIContent(message, icon), TCP2_GUI.HelpBoxRichTextStyle);
		//EditorGUI.HelpBox(position, this.message, this.msgType);

		if(Event.current != null && Event.current.type == EventType.Repaint)
			InspectorWidth = position.width;
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		//Calculate help box height
		bool scrollBar = (Screen.width - InspectorWidth) > 20;
		var height = TCP2_GUI.HelpBoxRichTextStyle.CalcHeight(new GUIContent(message, icon), Screen.width - (scrollBar ? 51 : 34));
		return height + 6f;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2SeparatorDecorator : MaterialPropertyDrawer
{
	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		position.y += 4;
		position.height -= 4;
		TCP2_GUI.Separator(position);
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return 12f;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2OutlineNormalsGUIDrawer : MaterialPropertyDrawer
{
	readonly GUIContent[] labels = new GUIContent[]
	{
			new GUIContent("Regular", "Use regular vertex normals"),
			new GUIContent("Vertex Colors", "Use vertex colors as normals (with smoothed mesh)"),
			new GUIContent("Tangents", "Use tangents as normals (with smoothed mesh)"),
			new GUIContent("UV2", "Use second texture coordinates as normals (with smoothed mesh)"),
	};
	readonly string[] keywords = new string[] { "TCP2_NONE", "TCP2_COLORS_AS_NORMALS", "TCP2_TANGENT_AS_NORMALS", "TCP2_UV2_AS_NORMALS" };

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		TCP2_GUI.Header("OUTLINE NORMALS", "Defines where to take the vertex normals from to draw the outline.\nChange this when using a smoothed mesh to fill the gaps shown in hard-edged meshes.");

		Rect r = EditorGUILayout.GetControlRect();
		r = EditorGUI.IndentedRect(r);
		int index = GetCurrentIndex(prop);
		EditorGUI.BeginChangeCheck();
		index = TCP2_GUI.RadioChoiceHorizontal(r, index, labels);
		if(EditorGUI.EndChangeCheck())
		{
			SetKeyword(prop, index);
		}
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return 0f;
	}

	int GetCurrentIndex(MaterialProperty prop)
	{
		int index = 0;
		Object[] targets = prop.targets;
		foreach(var t in targets)
		{
			Material m = (Material)t;
			for(int i = 0; i < keywords.Length; i++)
			{
				if(m.IsKeywordEnabled(keywords[i]))
				{
					return i;
				}
			}
		}
		return index;
	}

	private void SetKeyword(MaterialProperty prop, int index)
	{
		string label = prop.targets.Length > 1 ? string.Format("modify Outline Normals of {0} Materials", prop.targets.Length) : string.Format("modify Outline Normals of {0}", prop.targets[0].name);
		Undo.RecordObjects(prop.targets, label);
		for(int i = 0; i < this.keywords.Length; i++)
		{
			string keywordName = keywords[i];
			UnityEngine.Object[] targets = prop.targets;
			for(int j = 0; j < targets.Length; j++)
			{
				Material material = (Material)targets[j];
				if(index == i)
				{
					material.EnableKeyword(keywordName);
				}
				else
				{
					material.DisableKeyword(keywordName);
				}
			}
		}
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2Vector4FloatsDrawer : MaterialPropertyDrawer
{
	const float spacing = 2f;

	bool expanded;
	string groupLabel;
	string[] labels;
	float[] min;
	float[] max;
	bool useSlider;

	public TCP2Vector4FloatsDrawer(string groupLabel, string labelX, string labelY, string labelZ, string labelW)
	{
		this.groupLabel = groupLabel;
		this.labels = new string[] { labelX, labelY, labelZ, labelW };
		this.min = new float[] { float.MinValue, float.MinValue, float.MinValue, float.MinValue };
		this.max = new float[] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue, };
		this.useSlider = false;
		this.expanded = false;
	}
	public TCP2Vector4FloatsDrawer(string groupLabel, string labelX, string labelY, string labelZ, string labelW, float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float minW, float maxW)
	{
		this.groupLabel = groupLabel;
		this.labels = new string[] { labelX, labelY, labelZ, labelW };
		this.min = new float[] { SignedValue(minX), SignedValue(minY), SignedValue(minZ), SignedValue(minW) };
		this.max = new float[] { SignedValue(maxX), SignedValue(maxY), SignedValue(maxZ), SignedValue(maxW) };
		this.useSlider = true;
		this.expanded = false;
	}

	//hacky workaround because adding a minus sign in a material drawer argument will break the shader
	float SignedValue(float val)
	{
		return val > 90000 ? 90000 - val : val;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		Rect lineRect = position;
		lineRect.height = (lineRect.height-(expanded ? 5*spacing : 0)) / (expanded ? 5f : 1f);

		var values = prop.vectorValue;
		EditorGUI.BeginChangeCheck();
		this.expanded = EditorGUI.Foldout(lineRect, expanded, groupLabel);
		if(expanded)
		{
			lineRect.y += lineRect.height + spacing;

			var lw = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = position.width - 200f;

			values.x = useSlider ? EditorGUI.Slider(lineRect, labels[0], values.x, min[0], max[0]) : EditorGUI.FloatField(lineRect, labels[0], values.x);
			lineRect.y += lineRect.height + spacing;
			values.y = useSlider ? EditorGUI.Slider(lineRect, labels[1], values.y, min[1], max[1]) : EditorGUI.FloatField(lineRect, labels[1], values.y);
			lineRect.y += lineRect.height + spacing;
			values.z = useSlider ? EditorGUI.Slider(lineRect, labels[2], values.z, min[2], max[2]) : EditorGUI.FloatField(lineRect, labels[2], values.z);
			lineRect.y += lineRect.height + spacing;
			values.w = useSlider ? EditorGUI.Slider(lineRect, labels[3], values.w, min[3], max[3]) : EditorGUI.FloatField(lineRect, labels[3], values.w);

			EditorGUIUtility.labelWidth = lw;
		}
		if(EditorGUI.EndChangeCheck())
		{
			prop.vectorValue = values;
		}
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return (EditorGUIUtility.singleLineHeight+spacing) * (expanded ? 5 : 1) - spacing;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2GradientDrawer : MaterialPropertyDrawer
{
	static Texture2D DefaultRampTexture;
	static bool DefaultTextureSearched;     //Avoid searching each update if texture isn't found

	static private GUIContent editButtonLabel = new GUIContent("Edit Gradient", "Edit the ramp texture using Unity's gradient editor");
	static private GUIContent editButtonDisabledLabel = new GUIContent("Edit Gradient", "Can't edit the ramp texture because it hasn't been generated with the Ramp Generator\n\n(Tools/Toony Colors Pro 2/Ramp Generator)");

	private AssetImporter assetImporter;

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		//Find default ramp texture if needed
		if(prop.textureValue == null)
		{
			if(DefaultRampTexture == null && !DefaultTextureSearched)
			{
				DefaultRampTexture = TryFindDefaultRampTexture();
				DefaultTextureSearched = true;
			}

			if(DefaultRampTexture != null)
			{
				prop.textureValue = DefaultRampTexture;
			}
		}

		float indent = EditorGUI.indentLevel * 16;

		//Label
		var labelRect = position;
		labelRect.height = 16f;
		float space = labelRect.height + 4;
		position.y += space - 3;
		position.height -= space;
		EditorGUI.PrefixLabel(labelRect, new GUIContent(label));

		//Texture object field
		position.height = 16f;
		var newTexture = (Texture)EditorGUI.ObjectField(position, prop.textureValue, typeof(Texture2D), false);
		if(newTexture != prop.textureValue)
		{
			prop.textureValue = newTexture;
			assetImporter = null;
		}

		//Preview texture override (larger preview, hides texture name)
		var previewRect = new Rect(position.x + indent - 1, position.y + 1, position.width - indent - 18, position.height - 2);
		if(prop.hasMixedValue)
		{
			var col = GUI.color;
			GUI.color = EditorGUIUtility.isProSkin ? new Color(.25f, .25f, .25f) : new Color(.85f, .85f, .85f);
			EditorGUI.DrawPreviewTexture(previewRect, Texture2D.whiteTexture);
			GUI.color = col;
			GUI.Label(previewRect, "―");
		}
		else if(prop.textureValue != null)
			EditorGUI.DrawPreviewTexture(previewRect, prop.textureValue);

		if(prop.textureValue != null)
		{
			assetImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(prop.textureValue));
		}

		//Edit button
		var buttonRect = labelRect;
		buttonRect.width = 70;
		buttonRect.x = labelRect.x + (labelRect.width-150);
		if(GUI.Button(buttonRect, "Create New", EditorStyles.miniButtonLeft))
		{
			string lastSavePath = TCP2_GradientManager.LAST_SAVE_PATH;
			if(!lastSavePath.Contains(Application.dataPath))
				lastSavePath = Application.dataPath;

			string path = EditorUtility.SaveFilePanel("Create New Ramp Texture", lastSavePath, "TCP2_CustomRamp", "png");
			if(!string.IsNullOrEmpty(path))
			{
				TCP2_GradientManager.LAST_SAVE_PATH = System.IO.Path.GetDirectoryName(path);

				//Create texture and save PNG
				var projectPath = path.Replace(Application.dataPath, "Assets");
				TCP2_GradientManager.CreateAndSaveNewGradientTexture(256, projectPath);

				//Load created texture
				var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(projectPath);
				assetImporter = AssetImporter.GetAtPath(projectPath);

				//Assign to material(s)
				prop.textureValue = texture;

				//Open for editing
				TCP2_RampGenerator.OpenForEditing(texture, editor.targets, true);
			}
		}
		buttonRect.x += buttonRect.width;
		buttonRect.width = 80;
		bool enabled = GUI.enabled;
		GUI.enabled = (assetImporter != null) && assetImporter.userData.StartsWith("GRADIENT") && !prop.hasMixedValue;
		if(GUI.Button(buttonRect, GUI.enabled ? editButtonLabel : editButtonDisabledLabel, EditorStyles.miniButtonRight))
		{
			TCP2_RampGenerator.OpenForEditing((Texture2D)prop.textureValue, editor.targets, true);
		}
		GUI.enabled = enabled;

	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return 34f;
	}

	Texture2D TryFindDefaultRampTexture()
	{
		string rootPath = TCP2_Utils.FindReadmePath(true);
		if(!string.IsNullOrEmpty(rootPath))
		{
			string defaultTexPath = "Assets" + rootPath + "/Textures/TCP2_Ramp_3Levels.png";
			return AssetDatabase.LoadAssetAtPath<Texture2D>(defaultTexPath);
		}

		return null;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2HeaderDecorator : MaterialPropertyDrawer
{
	protected readonly string header;

	public TCP2HeaderDecorator(string header)
	{
		this.header = header;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		position.y += 2;
		TCP2_GUI.Header(position, header);
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return 18f;
	}
}

//---------------------------------------------------------------------------------------------------------------------

public class TCP2KeywordFilterDrawer : MaterialPropertyDrawer
{
	protected readonly string[] keywords;

	public TCP2KeywordFilterDrawer(string keyword)
	{
		this.keywords = keyword.Split(',');
	}

	public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
	{
		if(IsValid(editor))
		{
			EditorGUI.indentLevel++;
			editor.DefaultShaderProperty(prop, label);
			EditorGUI.indentLevel--;
		}
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		//There's still a small space if we return 0, -2 seems to get rid of that
		return -2f;
	}

	bool IsValid(MaterialEditor editor)
	{
		bool valid = false;
		if(editor.target != null && editor.target is Material)
		{
			foreach(var kw in keywords)
				valid |= (editor.target as Material).IsKeywordEnabled(kw);
		}
		return valid;
	}
}

//----------------------------------------------------------------------------------------------------------------------------------------------------------------
// Same as Toggle drawer, but doesn't set any keyword
// This will avoid adding unnecessary shader keyword to the project

internal class TCP2ToggleNoKeywordDrawer : MaterialPropertyDrawer
{
	private static bool IsPropertyTypeSuitable(MaterialProperty prop)
	{
		return prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range;
	}

	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		float result;
		if(!TCP2ToggleNoKeywordDrawer.IsPropertyTypeSuitable(prop))
		{
			result = 40f;
		}
		else
		{
			result = base.GetPropertyHeight(prop, label, editor);
		}
		return result;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
	{
		if(!TCP2ToggleNoKeywordDrawer.IsPropertyTypeSuitable(prop))
		{
			EditorGUI.HelpBox(position, "Toggle used on a non-float property: " + prop.name, MessageType.Warning);
		}
		else
		{
			EditorGUI.BeginChangeCheck();
			bool flag = Mathf.Abs(prop.floatValue) > 0.001f;
			EditorGUI.showMixedValue = prop.hasMixedValue;
			flag = EditorGUI.Toggle(position, label, flag);
			EditorGUI.showMixedValue = false;
			if(EditorGUI.EndChangeCheck())
			{
				prop.floatValue = ((!flag) ? 0f : 1f);
			}
		}
	}
}
