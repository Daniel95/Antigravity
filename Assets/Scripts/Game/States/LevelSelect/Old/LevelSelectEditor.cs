#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SelectableLevelFieldView))]
public class LevelSelectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SelectableLevelFieldView myScript = (SelectableLevelFieldView)target;
        if (GUILayout.Button("Build Level Select Fields"))
        {
            myScript.DestroyImmediateSelectableLevelFields();
            myScript.GenerateSelectableLevelFields();
        }
    }
}

#endif