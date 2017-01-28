using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public List<string> LevelNames { get; private set; }

    private void Awake()
    {
        LevelNames = GetLevelIndexes(GetComponent<SceneNamesLibrary>().SceneNames);
    }

    private static List<string> GetLevelIndexes(List<string> sceneNames)
    {
        List<string> levelNames = new List<string>();

        foreach (string sceneName in sceneNames)
        {
            if (!char.IsNumber(sceneName[sceneName.Length - 1]))
                continue;

            levelNames.Add(sceneName);
        }

        return levelNames;
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(LevelNames[levelNumber - 1]); 
    }

    public bool LevelExists(int levelNumber)
    {
        return levelNumber <= LevelNames.Count;
    }
}
