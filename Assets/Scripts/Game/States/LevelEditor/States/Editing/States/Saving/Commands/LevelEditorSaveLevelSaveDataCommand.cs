using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelEditorSaveLevelSaveDataCommand : Command {

    [Inject] private Refs<ILevelObject> levelObjectRefs;

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

        List<LevelObjectSaveData> levelObjectsSaveData = ExtractLevelObjectsSaveData();

        List<Vector2> standardTilePositions = LevelEditorTileGrid.Instance.GetGridPositionsByTileType(TileType.Standard);

        List<Vector2> tileGridPositions = LevelEditorTileGrid.Instance.GetTileGridPositions();
        List<Vector2> nonStandardTileGridPositions = tileGridPositions.Except(standardTilePositions).ToList();
        List<TileSaveData> nonStandardTileSaveData = ExtractTilesSaveData(nonStandardTileGridPositions);

        LevelSaveData levelData = new LevelSaveData {
            StandardTileGridPositions = standardTilePositions,
            NonStandardTilesSaveData = nonStandardTileSaveData,
            LevelObjectsSaveData = levelObjectsSaveData,
        };

        string levelFileName = StringHelper.ConvertToXMLCompatible(newLevelName);
        SerializeHelper.Serialize(LevelEditorLevelDataPath.Path + levelFileName, levelData);
    }

    private static List<TileSaveData> ExtractTilesSaveData(List<Vector2> gridPositions) {
        List<TileSaveData> tilesSaveData = new List<TileSaveData>();
        foreach (Vector2 gridPosition in gridPositions) {
            Tile tile = LevelEditorTileGrid.Instance.GetTile(gridPosition);

            TileSaveData tileSaveData = new TileSaveData {
                GridPosition = gridPosition,
                Rotation = tile.GameObject.transform.rotation,
                TileType = tile.TileType,
            };

            tilesSaveData.Add(tileSaveData);
        }

        return tilesSaveData;
    }

    private List<LevelObjectSaveData> ExtractLevelObjectsSaveData() {
        List<LevelObjectSaveData> levelObjectSaveDatas = new List<LevelObjectSaveData>();

        foreach (ILevelObject levelObject in levelObjectRefs) {
            LevelObjectSaveData levelObjectSaveData = new LevelObjectSaveData {
                Position = levelObject.GameObject.transform.position,
                Size = levelObject.GameObject.transform.localScale,
                Rotation = levelObject.GameObject.transform.rotation,
                LevelObjectType = levelObject.LevelObjectType,
            };
            levelObjectSaveDatas.Add(levelObjectSaveData);
        }

        return levelObjectSaveDatas;
    }

}