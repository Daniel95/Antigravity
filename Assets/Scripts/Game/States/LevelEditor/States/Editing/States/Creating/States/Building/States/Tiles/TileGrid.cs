using System.Collections.Generic;
using UnityEngine;

public static class TileGrid {

    public static Dictionary<Vector2, Tile> Grid { get { return grid; } }

    public static float NodeSize { get { return tileSize; } }

    private static Dictionary<Vector2, Tile> grid = new Dictionary<Vector2, Tile>();

    private static float tileSize;

    public static List<Vector2> GetGridPositions() {
        return new List<Vector2>(grid.Keys);
    }

    public static Tile GetTile(Vector2 gridPosition) {
        return grid[gridPosition];
    }

    public static List<Vector2> GetGridPositionsByTileType(TileType tileType) {
        List<Vector2> gridPositions = GetGridPositions();
        List<Vector2> gridPositionsWithType = gridPositions.FindAll(x => grid[x].TileType == tileType);

        return gridPositionsWithType;
    }

    public static void SetTile(Vector2 gridPosition, Tile tile) {
        if(grid.ContainsKey(gridPosition)) {
            RemoveTile(gridPosition);
            grid.Add(gridPosition, tile);
        } else {
            grid[gridPosition] = tile;
        }
    }

    public static void UpdateTile(Vector2 gridPosition, Tile tile) {
        Tile oldTile = grid[gridPosition];
        oldTile.Destroy();
        grid[gridPosition] = tile;
    }

    public static void AddTile(Vector2 gridPosition, Tile tile) {
        grid.Add(gridPosition, tile);
    }

    public static void Clear() {
        foreach (Vector2 gridPosition in grid.Keys) {
            grid[gridPosition].Destroy();
        }
        grid.Clear();
    }

    public static void RemoveTile(Vector2 gridPosition) {
        grid[gridPosition].Destroy();
        grid.Remove(gridPosition);
    }

    public static bool ContainsPosition(Vector2 gridPosition) {
        return grid.ContainsKey(gridPosition);
    }

    public static Vector2 WorldToGridPosition(Vector2 worldPosition) {
        Vector2 unroundedGridPosition = worldPosition / tileSize;
        Vector2 gridPosition = new Vector2(Mathf.Round(unroundedGridPosition.x), Mathf.Round(unroundedGridPosition.y));
        return gridPosition;
    }

    public static Vector2 GridToTilePosition(Vector2 gridPosition) {
        Vector2 tilePosition = gridPosition * tileSize;
        return tilePosition;
    }

    public static Vector2 WorldToTilePosition(Vector2 worldPosition) {
        Vector2 gridPosition = WorldToGridPosition(worldPosition);
        Vector2 tilePosition = GridToTilePosition(gridPosition);
        return tilePosition;
    }

    public static List<Vector2> GetSelection(Vector2 selectionStartGridPosition, Vector2 selectionEndGridPosition) {
        Vector2 offset = selectionEndGridPosition - selectionStartGridPosition;
        Vector2 direction = offset.normalized;
        Vector2 straightDirection = new Vector2(RoundingHelper.InvertOnNegativeCeil(direction.x), RoundingHelper.InvertOnNegativeCeil(direction.y));
        Vector2 selectionSize = new Vector2(Mathf.Abs(offset.x), Mathf.Abs(offset.y));

        List<Vector2> selection = new List<Vector2>();

        for (int xCounter = 0; xCounter <= selectionSize.x; xCounter++) {
            float xOffset = xCounter * straightDirection.x;
            float x = selectionStartGridPosition.x + xOffset;
            for (int yCounter = 0; yCounter <= selectionSize.y; yCounter++) {
                float yOffset = yCounter * straightDirection.y;
                float y = selectionStartGridPosition.y + yOffset;

                selection.Add(new Vector2(x, y));
            }
        }

        return selection;
    }

    public static Vector2 GetDirectionToAllDirectNeighbours(Vector2 gridPosition) {
        List<Vector2> allDirectNeighbourPositions = GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);

        Vector2 allDirectNeighboursDirection = GetDirectionToGridPositions(gridPosition, allDirectNeighbourPositions);
        return allDirectNeighboursDirection;
    }

    public static Vector2 GetDirectionToGridPositions(Vector2 gridPosition, List<Vector2> gridPositions) {
        Vector2 combinedOffsets = new Vector2();

        foreach (Vector2 directNeighbourPosition in gridPositions) {
            Vector2 offsetToNeighbour = directNeighbourPosition - gridPosition;
            combinedOffsets += offsetToNeighbour;
        }

        Vector2 neighbourDirection = RoundingHelper.InvertOnNegativeCeilMax(combinedOffsets, 1);

        return neighbourDirection;
    }

    public static List<Vector2> GetNeighbourPositions(Vector2 gridPosition, bool existing, NeighbourType neighbourType = NeighbourType.All, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        switch (neighbourType) {
            case NeighbourType.All:
                neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Direct:
                neighbourPositions = GetDirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Indirect:
                neighbourPositions = GetIndirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
        }

        return neighbourPositions;
    }

    public static Dictionary<Vector2, Tile> GetNeighbours(Vector2 gridPosition, NeighbourType neighbourType, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        switch (neighbourType) {
            case NeighbourType.All:
                neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Direct:
                neighbourPositions = GetDirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
            case NeighbourType.Indirect:
                neighbourPositions = GetIndirectNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
                break;
        }

        Dictionary<Vector2, Tile> neighbours = new Dictionary<Vector2, Tile>();
        neighbourPositions.ForEach(x => neighbours.Add(x, grid[x]));

        return neighbours;
    }

    public static List<Vector2> FindDirectNeighbourPositions(Vector2 gridPosition, List<Vector2> neighourPositions) {
        List<Vector2> neighbourPositions = neighourPositions.FindAll(x => gridPosition.x == x.x || gridPosition.y == x.y);

        return neighbourPositions;
    }

    public static List<Vector2> FindIndirectNeighbourPositions(Vector2 gridPosition, List<Vector2> neighourPositions) {
        List<Vector2> indirectNeighbourPositions = neighourPositions.FindAll(x => gridPosition.x != x.x && gridPosition.y != x.y);

        return indirectNeighbourPositions;
    }

    private static List<Vector2> GetAllNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - maxNeighbourOffset; x <= gridPosition.x + maxNeighbourOffset; x++) {
            for (int y = (int)gridPosition.y - maxNeighbourOffset; y <= gridPosition.y + maxNeighbourOffset; y++) {
                Vector2 neighbourPosition = new Vector2(x, y);
                if(neighbourPosition == gridPosition) { continue; }
                if(existing && !grid.ContainsKey(neighbourPosition)) { continue; }

                neighbourPositions.Add(neighbourPosition);
            }
        }

        return neighbourPositions;
    }

    private static List<Vector2> GetDirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> directNeighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - maxNeighbourOffset; x <= gridPosition.x + maxNeighbourOffset; x++) {
            Vector2 neighbourPosition = new Vector2(x, gridPosition.y);
            if (neighbourPosition == gridPosition) { continue; }
            if (existing && !grid.ContainsKey(neighbourPosition)) { continue; }

            directNeighbourPositions.Add(neighbourPosition);
        }

        for (int y = (int)gridPosition.y - maxNeighbourOffset; y <= gridPosition.y + maxNeighbourOffset; y++) {
            Vector2 neighbourPosition = new Vector2(gridPosition.x, y);
            if (neighbourPosition == gridPosition) { continue; }
            if (existing && !grid.ContainsKey(neighbourPosition)) { continue; }

            directNeighbourPositions.Add(neighbourPosition);
        }

        return directNeighbourPositions;
    }

    private static List<Vector2> GetIndirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
        List<Vector2> indirectNeighbourPositions = neighbourPositions.FindAll(x => gridPosition.x != x.x && gridPosition.y != x.y);

        return indirectNeighbourPositions;
    }

    public static void SetTileSize(float tileSize) {
        TileGrid.tileSize = tileSize;
    }

}
