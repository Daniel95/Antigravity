using System.Collections.Generic;
using UnityEngine;

public class TileGrid : LevelEditorGridPositions {

    public static TileGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, Tile> Grid { get { return grid; } }

    private static TileGrid instance;

    private Dictionary<Vector2, Tile> grid = new Dictionary<Vector2, Tile>();

    public Tile GetTile(Vector2 gridPosition) {
        return grid[gridPosition];
    }

    public List<Vector2> GetGridPositionsByTileType(TileType tileType) {
        List<Vector2> gridPositionsWithType = GridPositions.FindAll(x => grid[x].TileType == tileType);
        return gridPositionsWithType;
    }

    public void SetTile(Vector2 gridPosition, Tile tile) {
        if(grid.ContainsKey(gridPosition)) {
            RemoveTile(gridPosition);
            AddTile(gridPosition, tile);
        } else {
            grid[gridPosition] = tile;
        }
    }

    public void UpdateTile(Vector2 gridPosition, Tile tile) {
        Tile oldTile = grid[gridPosition];
        oldTile.Destroy();
        grid[gridPosition] = tile;
    }

    public void AddTile(Vector2 gridPosition, Tile tile) {
        Add(gridPosition);
        grid.Add(gridPosition, tile);
    }

    public override void Clear() {
        base.Clear();
        foreach (Vector2 gridPosition in grid.Keys) {
            grid[gridPosition].Destroy();
        }
        grid.Clear();
    }

    public void RemoveTile(Vector2 gridPosition) {
        Remove(gridPosition);
        grid[gridPosition].Destroy();
        grid.Remove(gridPosition);
    }

    public Vector2 GetDirectionToAllDirectNeighbours(Vector2 gridPosition) {
        List<Vector2> allDirectNeighbourPositions = GetNeighbourPositions(gridPosition, true, NeighbourType.Direct);

        Vector2 allDirectNeighboursDirection = GetDirectionToGridPositions(gridPosition, allDirectNeighbourPositions);
        return allDirectNeighboursDirection;
    }

    public Vector2 GetDirectionToGridPositions(Vector2 gridPosition, List<Vector2> gridPositions) {
        Vector2 combinedOffsets = new Vector2();

        foreach (Vector2 directNeighbourPosition in gridPositions) {
            Vector2 offsetToNeighbour = directNeighbourPosition - gridPosition;
            combinedOffsets += offsetToNeighbour;
        }

        Vector2 neighbourDirection = RoundingHelper.InvertOnNegativeCeilMax(combinedOffsets, 1);

        return neighbourDirection;
    }

    public List<Vector2> GetNeighbourPositions(Vector2 gridPosition, bool existing, NeighbourType neighbourType = NeighbourType.All, int maxNeighbourOffset = 1) {
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

    public Dictionary<Vector2, Tile> GetNeighbours(Vector2 gridPosition, NeighbourType neighbourType, bool existing, int maxNeighbourOffset = 1) {
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

    public List<Vector2> FindDirectNeighbourPositions(Vector2 gridPosition, List<Vector2> neighourPositions) {
        List<Vector2> neighbourPositions = neighourPositions.FindAll(x => gridPosition.x == x.x || gridPosition.y == x.y);

        return neighbourPositions;
    }

    public List<Vector2> FindIndirectNeighbourPositions(Vector2 gridPosition, List<Vector2> neighourPositions) {
        List<Vector2> indirectNeighbourPositions = neighourPositions.FindAll(x => gridPosition.x != x.x && gridPosition.y != x.y);

        return indirectNeighbourPositions;
    }

    private List<Vector2> GetAllNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
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

    private List<Vector2> GetDirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
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

    private List<Vector2> GetIndirectNeighbourPositions(Vector2 gridPosition, bool existing, int maxNeighbourOffset = 1) {
        List<Vector2> neighbourPositions = GetAllNeighbourPositions(gridPosition, existing, maxNeighbourOffset);
        List<Vector2> indirectNeighbourPositions = neighbourPositions.FindAll(x => gridPosition.x != x.x && gridPosition.y != x.y);

        return indirectNeighbourPositions;
    }

    private static TileGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<TileGrid>();
        }
        return instance;
    }

}
