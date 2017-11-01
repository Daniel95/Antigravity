using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCombinedStandardTilesCommand : Command {

    [Inject] private DeserializedLevelSaveDataStatus deserializedLevelSaveDataStatus;
    [Inject] private LevelContainerTransformStatus levelContainerStatus;

    protected override void Execute() {
        InstantiateStandardTiles(levelContainerStatus.LevelContainer);
    }

    private List<GameObject> InstantiateStandardTiles(Transform parent) {
        LevelSaveData levelSaveData = deserializedLevelSaveDataStatus.LevelSaveData;
        List<Vector2> standardTilePositions = levelSaveData.StandardTileGridPositions;

        float preRectangleSplitTime = Time.realtimeSinceStartup;
        List<List<Vector2>> rectangles = GridHelper.SortIntoRectangles(standardTilePositions);
        Debug.Log("Sorted " + standardTilePositions.Count + " grid positions into " + rectangles.Count + " rectangles. Time: " + (Time.realtimeSinceStartup - preRectangleSplitTime));

        List<GameObject> standardTileGameObjects = new List<GameObject>();
        foreach (List<Vector2> rectangle in rectangles) {
            GameObject standardTileGameObject = InstantiateRectangleStandardTile(rectangle, parent);
            standardTileGameObjects.Add(standardTileGameObject);
        }

        return standardTileGameObjects;
    }

    private GameObject InstantiateRectangleStandardTile(List<Vector2> rectangle, Transform parent) {
        Vector2 centerGridPosition = rectangle.Center();
        Vector2 startGridPosition = rectangle.Lowest();
        Vector2 endGridPosition = rectangle.Highest();

        Vector2 centerWorldPosition = LevelEditorGridHelper.GridToNodePosition(centerGridPosition);

        Vector2 startWorldPosition = LevelEditorGridHelper.GridToNodePosition(startGridPosition);
        Vector2 endWorldPosition = LevelEditorGridHelper.GridToNodePosition(endGridPosition);
        Vector2 offset = VectorHelper.Abs(endWorldPosition - startWorldPosition);

        float nodeSize = LevelEditorGridNodeSizeLibrary.Instance.NodeSize;

        float width = offset.x + nodeSize;
        float height = offset.y + nodeSize;
        Vector2 scale = new Vector2(width, height);

        GenerateableTileNode generatableTileNode = GenerateableTileLibrary.GetGeneratableTileNode(TileType.Standard);
        GameObject standardTile = Object.Instantiate(generatableTileNode.Prefab, centerWorldPosition, new Quaternion(), parent);
        standardTile.transform.localScale = scale;

        return standardTile;
    }

}