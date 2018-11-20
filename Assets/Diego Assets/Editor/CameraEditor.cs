using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(SnowDayCamera))]
public class CameraEditor : Editor {

    SerializedProperty PlayerList;

    private void OnEnable()
    {
        PlayerList = serializedObject.FindProperty("Players");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(PlayerList, true);

        SnowDayCamera camera = (SnowDayCamera)target;

        EditorGUILayout.LabelField("Rotation Resolution");
        camera.Resolution = EditorGUILayout.IntSlider(camera.Resolution, 0, 45);

        EditorGUILayout.LabelField("Rotation Step");
        camera.CurrentStep = EditorGUILayout.IntSlider(camera.CurrentStep, 0, camera.Resolution);

        camera.SmoothTime = EditorGUILayout.FloatField("Smooth Time", camera.SmoothTime);

        camera.XZLimister = EditorGUILayout.FloatField("XZ Limiter", camera.XZLimister);

        camera.YLimiter = EditorGUILayout.FloatField("Y Limiter", camera.YLimiter);

        camera.EdgeBorderBuffer = EditorGUILayout.FloatField("Edge Border Buffer", camera.EdgeBorderBuffer);




        






        

    }
}
