using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityToolbag;

public class TileGenerator : MonoBehaviour {

    public static TileGenerator Instance { get { return GetInstance(); } }
    public List<TileGeneratorNode> TileGeneratorNodes { get { return tileGeneratorNodes; } }

    public Vector2 TileSize { get { return GetTileSize(); } }

    private static TileGenerator instance;

    [SerializeField] [Reorderable] private List<TileGeneratorNode> tileGeneratorNodes = new List<TileGeneratorNode>();

    public static void SpawnTiles(List<Vector2> gridPositions) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.SetTile(x, new Tile() { UserGenerated = true }));

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allNeighbourPositions = LevelEditorTileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            List<Vector2> positionsToGenerate = allNeighbourPositions.Except(gridPositions).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = positionsToGenerate.FindAll(x => LevelEditorTileGrid.Instance.ContainsTile(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            positionsToGenerate.Add(gridPosition);
            Instance.GenerateTiles(positionsToGenerate);
        }
    }

    public static void RemoveTile(Vector2 gridPosition, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate = null) {
        LevelEditorTileGrid.Instance.RemoveTile(gridPosition);

        if (!regenerateNeighbours) { return; }

        List<Vector2> allNeighbourPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
        if (neighboursIgnoringRegenerate != null) {
            allNeighbourPositionsToRegenerate = allNeighbourPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        List<Vector2> nonUserGeneratedTilesToRegenerate = allNeighbourPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.ContainsTile(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
        nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        Instance.GenerateTiles(allNeighbourPositionsToRegenerate);
    }

    public static void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.ContainsTile(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    public static void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.ContainsTile(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    public static bool CheckGridPositionEmptyOrNotUserGenerated(Vector2 gridPosition) {
        bool empty = !LevelEditorTileGrid.Instance.Contains(gridPosition);
        if (empty) { return true; }
        bool tileNotUserGenerated = LevelEditorTileGrid.Instance.ContainsTile(gridPosition) && !LevelEditorTileGrid.Instance.GetTile(gridPosition).UserGenerated;
        return tileNotUserGenerated;
    }

    private void GenerateTile(Vector2 gridPosition) {
        TileGeneratorNode matchingTileGeneratorNode = null;

        for (int i = tileGeneratorNodes.Count - 1; i >= 0; i--) {
            TileGeneratorNode tileGeneratorNode = tileGeneratorNodes[i];
            TileCondition falseCondition = tileGeneratorNode.TileConditions.Find(x => !x.Check(gridPosition));

            if (falseCondition == null) {
                matchingTileGeneratorNode = tileGeneratorNode;
                break;
            }
        }

        Tile tile = GetTile(matchingTileGeneratorNode.Prefab, matchingTileGeneratorNode.TileType, gridPosition);
        if (tile.TileType == TileType.Empty) {
            if (LevelEditorTileGrid.Instance.ContainsTile(gridPosition)) {
                LevelEditorTileGrid.Instance.RemoveTile(gridPosition);
            }
        } else {
            LevelEditorTileGrid.Instance.SetTile(gridPosition, tile);

            foreach (TileAction tileAction in matchingTileGeneratorNode.TileActions) {
                tileAction.Do(gridPosition);
            }
        }
    }

    private void GenerateTiles(List<Vector2> gridPositions) {
        foreach (Vector2 gridPosition in gridPositions) {
            GenerateTile(gridPosition);
        }
    }

    private bool CheckTileTypeUserGenerated(TileType tileType) {
        TileGeneratorNode tileGeneratorNode = tileGeneratorNodes.Find(x =>  x.TileType == tileType);
        return tileGeneratorNode.UserGenerated;
    }

    private Tile GetTile(GameObject prefab, TileType tileType, Vector2 gridPosition) {
        if(tileType == TileType.Empty) { return new Tile() { TileType = TileType.Empty }; }

        Vector2 tilePosition = LevelEditorGridHelper.GridToNodePosition(gridPosition);

        GameObject tileGameObject = ObjectPool.Instance.GetObjectForType(prefab.name, false);
        tileGameObject.transform.position = tilePosition;
        bool userGenerated = CheckTileTypeUserGenerated(tileType);
        string tileName = "";

        if(!userGenerated) {
            tileName += "AutoGenerated ";
        }

        tileName += tileType.ToString() + " " + gridPosition.ToString();
        tileGameObject.name = tileName;

        Tile tile = new Tile() {
            TileType = tileType,
            GameObject = tileGameObject,
            UserGenerated = userGenerated,
        };

        return tile;
    }

    private void SetTilesGeneratorNodesBasedOnTileTypes() {
        Array enumArray = Enum.GetValues(typeof(TileType));
        int enumCount = enumArray.Length;

        if (tileGeneratorNodes.Count == enumCount) { return; }

        int index = 0;
        IEnumerable<TileType> TileTypeIEnumerable = enumArray.Cast<TileType>();

        foreach (TileType tileType in TileTypeIEnumerable) {
            if (tileGeneratorNodes.Count <= index) {
                tileGeneratorNodes.Insert(index, new TileGeneratorNode() {
                    TileType = tileType,
                });
            } else {
                tileGeneratorNodes[index].TileType = tileType;
            }
            index++;
        }
    }

    private static TileGenerator GetInstance() {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<TileGenerator>();
        }
        return instance;
    }

    private void Reset() {
        SetTilesGeneratorNodesBasedOnTileTypes();
    }

    private void OnValidate() {
        SetTilesGeneratorNodesBasedOnTileTypes();
    }

    private Vector2 GetTileSize() {
        float tileWidth = tileGeneratorNodes.Find(x => x.TileType == TileType.Standard).Prefab.transform.localScale.x;
        float tileHeight = tileWidth;
        Vector2 tileSize = new Vector2(tileWidth, tileHeight);
        return tileSize;
    }

}
