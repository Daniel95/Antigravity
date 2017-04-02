using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNamesLibrary : MonoBehaviour
{
    [SerializeField]
    private List<string> sceneNames;

    #if UNITY_EDITOR

    public void UpdateNames()
    {
        sceneNames = ReadNames();
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

    public List<string> SceneNames {
        get { return sceneNames; }
    }
}