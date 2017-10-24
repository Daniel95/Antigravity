using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNonStandardTilesCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;

    protected override void Execute() {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;
        List<Vector2> userGeneratedTileGridPositions = levelSaveData.UserGeneratedTileGridPositions;

        Dictionary<Vector2, TileType> gridPositionsByTileType = TileGenerator.GenerateFakeTiles(userGeneratedTileGridPositions);
        List<Vector2> nonStandardTilePositions = new List<Vector2>();
        foreach (KeyValuePair<Vector2, TileType> gridPositionByTileType in gridPositionsByTileType) {
            if (gridPositionByTileType.Value == TileType.Standard) { continue; }
            GameObject prefab = GenerateableTileLibrary.GeneratableTiles.Find(x => x.TileType == gridPositionByTileType.Value).Prefab;
            nonStandardTilePositions.Add(gridPositionByTileType.Key);
        }

        TileGenerator.SpawnTiles(nonStandardTilePositions);
    }

}
