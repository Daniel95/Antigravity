#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SceneNamesLibrary))]
public class SceneNamesLibraryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SceneNamesLibrary myScript = (SceneNamesLibrary)target;
        if (GUILayout.Button("Save scene names"))
        {
            myScript.UpdateNames();
        }
    }
}

#endif