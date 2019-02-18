using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SnowDayCamera))]
public class CameraEditor : Editor
{
    private SnowDayCamera cameraScript;
    private SerializedObject Target;

    private SerializedProperty PlayerList;
    private SerializedProperty CameraRotResolution;
    private SerializedProperty CameraRotStep;
    private SerializedProperty SmoothTime;
    private SerializedProperty XZMultiplier;
    private SerializedProperty YMultiplier;
    private SerializedProperty EdgeBorderBuffer;
    private SerializedProperty CameraYOffset;
    private void OnEnable()
    {
        cameraScript = (SnowDayCamera)target;
        Target = new SerializedObject(cameraScript);

        PlayerList = Target.FindProperty("Players");
        CameraRotResolution = Target.FindProperty("Resolution");
        CameraRotStep = Target.FindProperty("CurrentStep");
        SmoothTime = Target.FindProperty("SmoothTime");
        CameraYOffset = Target.FindProperty("CameraYOffset");
        XZMultiplier = Target.FindProperty("XZMultiplier");
        YMultiplier = Target.FindProperty("YMultiplier");
        EdgeBorderBuffer = Target.FindProperty("EdgeBorderBuffer");
    }

    public override void OnInspectorGUI()
    {
        Target.Update();

        EditorGUILayout.PropertyField(PlayerList, true);

        EditorGUILayout.LabelField(new GUIContent("Camera Rotation Resolution", "Steps the camera will be allowed to travel"));
        EditorGUILayout.IntSlider(CameraRotResolution, 1, 45 , new GUIContent("", ""));
        EditorGUILayout.IntSlider(CameraRotStep, 1, CameraRotResolution.intValue, new GUIContent("Step", "Camera Rotation Step"));
        EditorGUILayout.PropertyField(SmoothTime);
        //EditorGUILayout.PropertyField(CameraYOffset);

        EditorGUILayout.PropertyField(XZMultiplier);
        EditorGUILayout.PropertyField(YMultiplier);
        EditorGUILayout.PropertyField(EdgeBorderBuffer);

        Target.ApplyModifiedProperties();
    }
}