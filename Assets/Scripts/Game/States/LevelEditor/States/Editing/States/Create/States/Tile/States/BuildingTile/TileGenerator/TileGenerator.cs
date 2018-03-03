using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TileGenerator {

    public static void SpawnTiles(List<Vector2> gridPositions) {
        gridPositions.ForEach(x => TileGrid.Instance.SetTile(x, new Tile() { UserGenerated = true }));

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allNeighbourPositions = TileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            List<Vector2> positionsToGenerate = allNeighbourPositions.Except(gridPositions).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = positionsToGenerate.FindAll(x => TileGrid.Instance.ContainsTile(x) && !TileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.Instance.RemoveTile(x));

            positionsToGenerate.Add(gridPosition);
            GenerateTiles(positionsToGenerate);
        }
    }

    public static void RemoveTile(Vector2 gridPosition, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate = null) {
        TileGrid.Instance.RemoveTile(gridPosition);

        if (!regenerateNeighbours) { return; }

        List<Vector2> allNeighbourPositionsToRegenerate = TileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
        if (neighboursIgnoringRegenerate != null) {
            allNeighbourPositionsToRegenerate = allNeighbourPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        List<Vector2> nonUserGeneratedTilesToRegenerate = allNeighbourPositionsToRegenerate.FindAll(x => TileGrid.Instance.ContainsTile(x) && !TileGrid.Instance.GetTile(x).UserGenerated);
        nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.Instance.RemoveTile(x));

        GenerateTiles(allNeighbourPositionsToRegenerate);
    }

    public static void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate) {
        gridPositions.ForEach(x => TileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = TileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => TileGrid.Instance.ContainsTile(x) && !TileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.Instance.RemoveTile(x));

            GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    public static void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours) {
        gridPositions.ForEach(x => TileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = TileGrid.Instance.GetNeighbourTilePositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => TileGrid.Instance.ContainsTile(x) && !TileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.Instance.RemoveTile(x));

            GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    public static bool CheckGridPositionEmptyOrNotUserGenerated(Vector2 gridPosition) {
        bool empty = !TileGrid.Instance.Contains(gridPosition);
        if (empty) { return true; }
        bool tileNotUserGenerated = TileGrid.Instance.ContainsTile(gridPosition) && !TileGrid.Instance.GetTile(gridPosition).UserGenerated;
        return tileNotUserGenerated;
    }

    private static void GenerateTile(Vector2 gridPosition) {
        GenerateableTileNode matchingTileGeneratorNode = GetGeneratableTileNode(gridPosition);

        Tile tile = GetTile(matchingTileGeneratorNode.Prefab, matchingTileGeneratorNode.TileType, gridPosition);
        if (tile.TileType == TileType.Empty) {
            if (TileGrid.Instance.ContainsTile(gridPosition)) {
                TileGrid.Instance.RemoveTile(gridPosition);
            }
        } else {
            TileGrid.Instance.SetTile(gridPosition, tile);

            foreach (TileAction tileAction in matchingTileGeneratorNode.TileActions) {
                tileAction.Do(gridPosition);
            }
        }
    }

    private static void GenerateTiles(List<Vector2> gridPositions) {
        foreach (Vector2 gridPosition in gridPositions) {
            GenerateTile(gridPosition);
        }
    }

    private static bool CheckTileTypeUserGenerated(TileType tileType) {
        GenerateableTileNode generatableTile = GenerateableTileLibrary.GetGeneratableTileNode(tileType);
        return generatableTile.UserGenerated;
    }

    private static GenerateableTileNode GetGeneratableTileNode(Vector2 gridPosition) {
        GenerateableTileNode matchingTileGeneratorNode = null;
        List<GenerateableTileNode> tileEditorNodes = GenerateableTileLibrary.GenerateableTiles;

        for (int i = tileEditorNodes.Count - 1; i >= 0; i--) {
            GenerateableTileNode tileGeneratorNode = tileEditorNodes[i];
            bool falseConditionFound = tileGeneratorNode.TileConditions.Exists(x => !x.Check(gridPosition));
            if (!falseConditionFound) {
                matchingTileGeneratorNode = tileGeneratorNode;
                break;
            }
        }

        return matchingTileGeneratorNode;
    }

    private static Tile GetTile(GameObject prefab, TileType tileType, Vector2 gridPosition) {
        if (tileType == TileType.Empty) { return new Tile() { TileType = TileType.Empty }; }

        Vector2 tilePosition = LevelEditorGridHelper.GridToNodePosition(gridPosition);

        GameObject tileGameObject = ObjectPool.Instance.GetObjectForType(prefab.name, false);
        tileGameObject.transform.position = tilePosition;
        bool userGenerated = CheckTileTypeUserGenerated(tileType);
        string tileName = "";

        if (!userGenerated) {
            tileName += "AutoGenerated Tile ";
        } else {
            tileName += "Tile ";
        }

        tileName += tileType.ToString() + " " + gridPosition.ToString();
        tileGameObject.name = tileName;

        tileGameObject.layer = LayerMask.NameToLayer(Layers.LevelEditorTile);

        Tile tile = new Tile() {
            TileType = tileType,
            GameObject = tileGameObject,
            UserGenerated = userGenerated,
        };

        return tile;
    }

}
