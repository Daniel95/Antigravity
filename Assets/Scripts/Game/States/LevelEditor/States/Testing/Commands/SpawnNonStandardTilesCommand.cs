using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnNonStandardTilesCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;

    protected override void Execute() {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;
        List<TileSaveData> nonStandardUserGeneratedTilesSaveData = levelSaveData.NonStandardUserGeneratedTilesSaveData;
        List<TileSaveData> nonStandardNonUserGeneratedTilesSaveData = levelSaveData.NonStandardNonUserGeneratedTilesSaveData;
        List<TileSaveData> nonStandardTilesSaveData = nonStandardUserGeneratedTilesSaveData.Concat(nonStandardNonUserGeneratedTilesSaveData).ToList();

        foreach (TileSaveData nonStandardTileSaveData in nonStandardTilesSaveData) {
            GameObject prefab = GenerateableTileLibrary.GetGeneratableTileNode(nonStandardTileSaveData.TileType).Prefab;
            Vector2 position = LevelEditorGridHelper.GridToNodePosition(nonStandardTileSaveData.GridPosition);
            Quaternion rotation = nonStandardTileSaveData.Rotation;

            GameObject nonStandardTile = Object.Instantiate(prefab, position, rotation);

            Vector2 size = new Vector2(LevelEditorGridNodeSize.Instance.NodeSize, LevelEditorGridNodeSize.Instance.NodeSize);
            nonStandardTile.transform.localScale = size;
            nonStandardTile.transform.SetParent(levelContainerStatus.LevelContainer);
        }
    }

}
