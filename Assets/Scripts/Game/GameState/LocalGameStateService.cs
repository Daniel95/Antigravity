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
        SerializeHelper.Serialize(GetFullPath(), gameSave);
    }

    private string GetFullPath() {
        return Application.persistentDataPath + SAVE_GAME_PATH;
    }

}