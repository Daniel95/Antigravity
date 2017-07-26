using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGrid {

    public static Dictionary<Vector2, TileType> Grid { get { return grid; } set { grid = value; } }

    private static Dictionary<Vector2, TileType> grid = new Dictionary<Vector2, TileType>();

    public static Vector2 GetGridPosition(Vector2 position) {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
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
}
