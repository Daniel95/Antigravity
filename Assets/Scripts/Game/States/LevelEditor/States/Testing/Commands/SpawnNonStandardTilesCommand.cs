using IoCPlus;
using UnityEngine;

public class SpawnNonStandardTilesCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;

    protected override void Execute() {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;
        foreach (TileSaveData nonStandardTileSaveData in levelSaveData.NonStandardTilesSaveData) {
            GameObject prefab = GenerateableTileLibrary.GetGeneratableTileNode(nonStandardTileSaveData.TileType).Prefab;
            Vector2 position = LevelEditorGridHelper.GridToNodePosition(nonStandardTileSaveData.GridPosition);
            Quaternion rotation = nonStandardTileSaveData.Rotation;

            GameObject nonStandardTile = Object.Instantiate(prefab, position, rotation);

            Vector2 size = new Vector2(LevelEditorGridNodeSizeLibrary.Instance.NodeSize, LevelEditorGridNodeSizeLibrary.Instance.NodeSize);
            nonStandardTile.transform.localScale = size;
            nonStandardTile.transform.SetParent(levelContainerStatus.LevelContainer);
        }
    }

}
