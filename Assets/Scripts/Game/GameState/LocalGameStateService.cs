using System.IO;
using UnityEngine;

public class LocalGameStateService : IGameStateService {

    private const string SAVE_GAME_PATH = "/GameSaves/GameSave.xml";

    public GameStateModel Load() {
        if (!File.Exists(GetFullPath())) {
            return new GameStateModel();
        }

        GameStateModel gameStateModel = SerializeHelper.Deserialize<GameStateModel>(GetFullPath());

        if (gameStateModel == null) {
            Debug.LogError("Could not deserialize game save.");
            return new GameStateModel();
        }

        return gameStateModel;
    }

    public void Save(GameStateModel gameSave) {
        CreateDirectoryIfNeeded();
        SerializeHelper.Serialize(GetFullPath(), gameSave);
    }

    private string GetFullPath() {
        return Application.persistentDataPath + SAVE_GAME_PATH;
    }

    private void CreateDirectoryIfNeeded() {
        string fullPath = GetFullPath();
        int index = fullPath.LastIndexOf('/');
        if (index == 0) { return; }
        string directory = fullPath.Substring(0, index);
        Directory.CreateDirectory(directory);
    }

}