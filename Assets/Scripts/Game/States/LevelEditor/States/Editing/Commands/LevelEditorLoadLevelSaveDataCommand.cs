using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorLoadLevelSaveDataCommand : Command {

    [Inject] private IContext context;

    [Inject] private LevelContainerTransformStatus levelContainerStatus;
    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        string levelFileName = StringHelper.ConvertToXMLCompatible(levelNameStatus.Name);

        LevelSaveData levelSaveData = SerializeHelper.Deserialize<LevelSaveData>(LevelEditorLevelDataPath.Path + levelFileName);

        List<Vector2> standardTileGridPositions = levelSaveData.StandardTileGridPositions;

        List<Vector2> nonStandardTileGridPositions = new List<Vector2>();
        foreach (TileSaveData tileSaveData in levelSaveData.NonStandardTilesSaveData) {
            GenerateableTileNode generateableTileNode = GenerateableTileLibrary.GetGeneratableTileNode(tileSaveData.TileType);
            if(!generateableTileNode.UserGenerated) { continue; }
            nonStandardTileGridPositions.Add(tileSaveData.GridPosition);
        }

        List<Vector2> userGeneratedTileGridPositions = standardTileGridPositions.Concat(nonStandardTileGridPositions).ToList();
        TileGenerator.SpawnTiles(userGeneratedTileGridPositions);

        List<LevelObjectSaveData> levelObjectsSaveData = levelSaveData.LevelObjectsSaveData;
        SpawnLevelObjects(levelObjectsSaveData);
    }

    private void SpawnLevelObjects(List<LevelObjectSaveData> levelObjectsSaveData) {
        foreach (LevelObjectSaveData levelObjectSaveData in levelObjectsSaveData) {
            LevelObjectType levelObjectType = levelObjectSaveData.LevelObjectType;
            GenerateableLevelObjectNode levelEditorLevelObjectEditorNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
            Vector2 position = levelObjectSaveData.Position;

            GameObject levelObject = LevelObjectHelper.InstantiateLevelObject(levelEditorLevelObjectEditorNode, position, context);
            levelObject.transform.localScale = levelObjectSaveData.Size;
            levelObject.transform.rotation = levelObjectSaveData.Rotation;
        }
    }

}
