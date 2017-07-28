using System.Collections.Generic;
using UnityEngine;

public static class TileGrid {

    public static float NodeSize { get { return tileSize; } }

    public static Dictionary<Vector2, Tile> Grid { get { return grid; } set { grid = value; } }

    private static Dictionary<Vector2, Tile> grid = new Dictionary<Vector2, Tile>();

    private static float tileSize;

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

    public static List<Vector2> GetGridPositionNeighbourPositions(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - 1; x <= gridPosition.x + 1; x += 2) {
            for (int y = (int)gridPosition.y - 1; y <= gridPosition.y + 1; y += 2) {
                Vector2 neighbourPosition = new Vector2(x, y);
                neighbourPositions.Add(neighbourPosition);
            }
        }

        return neighbourPositions;
    }

    public static List<Vector2> GetGridPositionDirectNeighbourPositions(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = new List<Vector2>();

        for (int x = (int)gridPosition.x - 1; x <= gridPosition.x + 1; x += 2) {
            Vector2 neighbourPosition = new Vector2(x, gridPosition.y);
            directNeighbourPositions.Add(neighbourPosition);
        }

        for (int y = (int)gridPosition.y - 1; y <= gridPosition.y + 1; y += 2) {
            Vector2 neighbourPosition = new Vector2(gridPosition.x, y);
            directNeighbourPositions.Add(neighbourPosition);
        }

        return directNeighbourPositions;
    }

    public static Dictionary<Vector2, Tile> GetGridPositionNeighbours(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = GetGridPositionNeighbourPositions(gridPosition);
        Dictionary<Vector2, Tile> neighbours = GetExistingTilesInGridPositions(neighbourPositions);

        return neighbours;
    }

    public static Dictionary<Vector2, Tile> GetGridPositionDirectNeighbours(Vector2 gridPosition) {
        List<Vector2> directNeighbourPositions = GetGridPositionDirectNeighbourPositions(gridPosition);
        Dictionary<Vector2, Tile> directNeighbours = GetExistingTilesInGridPositions(directNeighbourPositions);

        return directNeighbours;
    }

    private static Dictionary<Vector2, Tile> GetExistingTilesInGridPositions(List<Vector2> gridPositions) {
        Dictionary<Vector2, Tile> existingTiles = new Dictionary<Vector2, Tile>();

        foreach (Vector2 neighbourPosition in gridPositions) {
            if (!grid.ContainsKey(neighbourPosition)) { continue; }
            existingTiles.Add(neighbourPosition, grid[neighbourPosition]);
        }

        return existingTiles;
    }

    public static void SetTileSize(float tileSize) {
        TileGrid.tileSize = tileSize;
    }
}
