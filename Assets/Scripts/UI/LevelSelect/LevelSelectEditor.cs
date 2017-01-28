using UnityEngine;
using System.Collections;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(LevelSelectField))]
public class LevelSelectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelSelectField myScript = (LevelSelectField)target;
        if (GUILayout.Button("Build Level Select Fields"))
        {
            myScript.DestroyImmediateChildren();
            myScript.BuildFields();
        }
    }
}

#endif