using System.Collections.Generic;
using UnityEngine;

public static class TileGrid {

    public static float NodeSize { get { return tileSize; } }

    public static Dictionary<Vector2, TileType> Grid { get { return grid; } set { grid = value; } }

    private static Dictionary<Vector2, TileType> grid = new Dictionary<Vector2, TileType>();

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

    public static Dictionary<Vector2, TileType> GetGridPositionNeighbours(Vector2 gridPosition) {
        Dictionary<Vector2, TileType> neighbours = new Dictionary<Vector2, TileType>();

        for (int x = (int)gridPosition.x - 1; x <= gridPosition.x + 1; x += 2) {
            Vector2 neighbourPosition = new Vector2(x, gridPosition.y);
            neighbours.Add(neighbourPosition, grid[neighbourPosition]);
        }

        for (int y = (int)gridPosition.y - 1; y <= gridPosition.y + 1; y += 2) {
            Vector2 neighbourPosition = new Vector2(gridPosition.x, y);
            neighbours.Add(neighbourPosition, grid[neighbourPosition]);
        }

        return neighbours;
    }

    public static void SetTileSize(float tileSize) {
        TileGrid.tileSize = tileSize;
    }
}
