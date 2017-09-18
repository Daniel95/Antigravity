﻿using UnityEngine;

public class LevelEditorLevelDataPath {

    public static string Path { get { return GetLevelDataPath(); } }

    private static string GetLevelDataPath() {
        return Application.persistentDataPath + "/Data/Levels/";
    }

}
