using model;
using System;
using UnityEditor;

[CustomEditor(typeof(BallData))]
public class BallScriptableObjectEditor : Editor
{
    private SerializedProperty speedProperty;
    private SerializedProperty childData;
    private SerializedProperty splitAmount;


    private void OnEnable()
    {
        speedProperty = serializedObject.FindProperty("speed");
        childData = serializedObject.FindProperty("childData");
        splitAmount = serializedObject.FindProperty("splitAmount");


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        BallData ballData = (BallData)target;
        BallsConfig ballsConfig = GetBallsConfig();

        if (ballsConfig != null)
        {
            string[] sizeOptions = Array.ConvertAll(ballsConfig.BallSizes, x => x.ToString());
            int selectedIndex = Array.IndexOf(ballsConfig.BallSizes, ballData.Size);

            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUILayout.Popup("Size", selectedIndex, sizeOptions);
            if (EditorGUI.EndChangeCheck())
            {
                ballData.Size = ballsConfig.BallSizes[selectedIndex];
            }
        }

        EditorGUILayout.PropertyField(speedProperty);
        EditorGUILayout.PropertyField(childData);
        EditorGUILayout.PropertyField(splitAmount);


        serializedObject.ApplyModifiedProperties();
    }

    private BallsConfig GetBallsConfig()
    {
        string[] guids = AssetDatabase.FindAssets("t:BallsConfig");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<BallsConfig>(path);
        }
        return null;
    }
}