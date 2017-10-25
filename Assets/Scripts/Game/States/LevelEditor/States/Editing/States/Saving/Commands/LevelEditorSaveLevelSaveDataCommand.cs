using IoCPlus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelEditorSaveLevelSaveDataCommand : Command {

    [Inject] private LevelEditorOffGridLevelObjectsStatus offGridLevelObjectsStatus;

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

        List<OnGridLevelObjectSaveData> onGridLevelObjectsSaveData = ExtractOnGridLevelObjectsSaveData();
        List<OffGridLevelObjectSaveData> offGridLevelObjectsSaveData = ExtractOffGridLevelObjectsSaveData();

        List<Vector2> standardTilePositions = LevelEditorTileGrid.Instance.GetGridPositionsByTileType(TileType.Standard);

        List<Vector2> tileGridPositions = LevelEditorTileGrid.Instance.GetTileGridPositions();
        List<Vector2> nonStandardTileGridPositions = tileGridPositions.Except(standardTilePositions).ToList();
        List<TileSaveData> nonStandardTileSaveData = ExtractTilesSaveData(nonStandardTileGridPositions);

        LevelSaveData levelData = new LevelSaveData {
            StandardTileGridPositions = standardTilePositions,
            NonStandardTilesSaveData = nonStandardTileSaveData,
            OnGridLevelObjectsSaveData = onGridLevelObjectsSaveData,
            OffGridLevelObjectsSaveData = offGridLevelObjectsSaveData,
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

    private static List<OnGridLevelObjectSaveData> ExtractOnGridLevelObjectsSaveData() {
        List<OnGridLevelObjectSaveData> onGridLevelObjectSaveDatas = new List<OnGridLevelObjectSaveData>();
        List<OnGridLevelObject> levelObjects = OnGridLevelObject.OnGridLevelObjects;

        foreach (OnGridLevelObject levelObject in levelObjects) {
            OnGridLevelObjectSaveData levelObjectSaveData = new OnGridLevelObjectSaveData {
                GridPositions = levelObject.GridPositions,
                GameObjectPosition = levelObject.GameObjectPosition,
                LevelObjectType = levelObject.LevelObjectType,
            };
            onGridLevelObjectSaveDatas.Add(levelObjectSaveData);
        }

        return onGridLevelObjectSaveDatas;
    }

    private List<OffGridLevelObjectSaveData> ExtractOffGridLevelObjectsSaveData() {
        List<OffGridLevelObjectSaveData> offGridLevelObjectSaveDatas = new List<OffGridLevelObjectSaveData>();

        Dictionary<GameObject, LevelObjectType> OffGridLevelObjectsByGameObject = offGridLevelObjectsStatus.OffGridLevelObjectsByGameObject;
        foreach (KeyValuePair<GameObject, LevelObjectType> levelObjectTypeByGameObject in OffGridLevelObjectsByGameObject) {
            Transform offGridLevelObjectTransform = levelObjectTypeByGameObject.Key.transform;
            OffGridLevelObjectSaveData offGridLevelObjectSaveData = new OffGridLevelObjectSaveData {
                Position = offGridLevelObjectTransform.position,
                Size = offGridLevelObjectTransform.localScale,
                LevelObjectType = levelObjectTypeByGameObject.Value,
            };
            offGridLevelObjectSaveDatas.Add(offGridLevelObjectSaveData);
        }

        return offGridLevelObjectSaveDatas;
    }

}