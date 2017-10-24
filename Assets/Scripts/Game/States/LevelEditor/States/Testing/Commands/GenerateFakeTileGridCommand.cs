using UnityEngine;
using System.Collections;
using IoCPlus;
using System.Collections.Generic;

public class GenerateFakeTileGridCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;

    protected override void Execute() {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;
        List<Vector2> userGeneratedTileGridPositions = levelSaveData.UserGeneratedTileGridPositions;

        TileGenerator.GenerateFakeTiles(userGeneratedTileGridPositions);
    }

}
