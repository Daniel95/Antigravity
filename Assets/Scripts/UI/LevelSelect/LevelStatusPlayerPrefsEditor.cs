﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelStatusPlayerPrefs))]
public class LevelStatusPlayerPrefsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Delete Saved Data"))
        {
            LevelStatusPlayerPrefs.DeleteAllSavedData();
        }
    }
}