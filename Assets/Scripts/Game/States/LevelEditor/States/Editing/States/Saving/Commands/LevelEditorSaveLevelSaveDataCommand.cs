using IoCPlus;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelEditorSaveLevelSaveDataCommand : Command {
    
    [Inject] private LevelNameStatus levelNameStatus;

    [InjectParameter] private string newLevelName;

    protected override void Execute() {
        string previousLevelName = levelNameStatus.Name;

        bool levelWasRenamed = !string.IsNullOrEmpty(previousLevelName) && newLevelName != previousLevelName;
        if (levelWasRenamed) {
            string oldLevelFileName = StringHelper.ConvertToXMLCompatible(previousLevelName);

            if (File.Exists(LevelEditorLevelDataPath.Path + oldLevelFileName)) {
                File.Delete(LevelEditorLevelDataPath.Path + oldLevelFileName);
            }
        }

        string levelFileName = StringHelper.ConvertToXMLCompatible(newLevelName);

        List<Vector2> userGeneratedTileGridPositions = LevelEditorTileGrid.Instance.GetUserGeneratedTileGridPositions();
        List<LevelObjectSaveData> levelObjectSaveDatas = new List<LevelObjectSaveData>();

        List<LevelObject> levelObjects = LevelObject.LevelObjects;

        foreach(LevelObject levelObject in levelObjects) {
            LevelObjectSaveData levelObjectSaveData = new LevelObjectSaveData {
                GridPositions = levelObject.GridPositions,
                GameObjectPosition = levelObject.GameObjectPosition,
                LevelObjectType = levelObject.LevelObjectType,
            };
            levelObjectSaveDatas.Add(levelObjectSaveData);
        }

        LevelSaveData levelData = new LevelSaveData {
            UserGeneratedTileGridPositions = userGeneratedTileGridPositions,
            LevelObjectSaveDatas = levelObjectSaveDatas,
        };

        SerializeHelper.Serialize(LevelEditorLevelDataPath.Path + levelFileName, levelData);
    }

}