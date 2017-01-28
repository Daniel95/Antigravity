using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNamesLibrary : MonoBehaviour
{
    public List<string> SceneNames;

    #if UNITY_EDITOR

    public void UpdateNames()
    {
        SceneNames = ReadNames();
    }

    private static List<string> ReadNames()
    {
        List<string> temp = new List<string>();

        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;

            string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
            name = name.Substring(0, name.Length - 6);
            temp.Add(name);
        }

        return temp;
    }
    #endif
}