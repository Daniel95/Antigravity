using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileGrid : LevelEditorGrid {

    public static TileGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, Tile> Grid { get { return grid; } }

    private static TileGrid instance;

    private Dictionary<Vector2, Tile> grid = new Dictionary<Vector2, Tile>();

    public Tile GetTile(Vector2 gridPosition) {
        return grid[gridPosition];
    }

    public List<Vector2> GetTileGridPositions() {
        List<Vector2> tileGridPositions = grid.Keys.ToList();
        return tileGridPositions;
    }

    public List<Vector2> GetUserGeneratedTileGridPositions() {
        List<Vector2> userGeneratedTileGridPosition = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Tile> tileByGridPosition in grid) {
            if(tileByGridPosition.Value.UserGenerated) {
                userGeneratedTileGridPosition.Add(tileByGridPosition.Key);
            }
        }

        return userGeneratedTileGridPosition;
    }

    public List<Vector2> GetNonUserGeneratedTileGridPositions() {
        List<Vector2> userGeneratedTileGridPosition = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Tile> tileByGridPosition in grid) {
            if (!tileByGridPosition.Value.UserGenerated) {
                userGeneratedTileGridPosition.Add(tileByGridPosition.Key);
            }
        }

        return userGeneratedTileGridPosition;
    }

    public List<Vector2> GetGridPositionsByTileType(TileType tileType) {
        List<Vector2> gridPositionsWithType = grid.Keys.ToList().FindAll(x => grid[x].TileType == tileType);
        return gridPositionsWithType;
    }

    public void SetTile(Vector2 gridPosition, Tile tile) {
        if (Contains(gridPosition)) {
            RemoveTile(gridPosition);
        }
        AddTile(gridPosition, tile);
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

    public bool ContainsTile(Vector2 gridPositions) {
        bool contains = grid.ContainsKey(gridPositions);
        return contains;
    }

    public bool CheckIsEmptyOrTile(Vector2 gridPosition) {
        bool empty = !Contains(gridPosition);
        if (empty) { return true; }
        bool isTile = ContainsTile(gridPosition);
        return isTile;
    }

    public List<Vector2> FilterNonEmptyOrNonTiles(List<Vector2> gridPositions) {
        List<Vector2> tileGridPositions = new List<Vector2>(gridPositions);
        tileGridPositions.RemoveAll(x => !CheckIsEmptyOrTile(x));
        return tileGridPositions;
    }

    public void RemoveTile(Vector2 gridPosition) {
        Remove(gridPosition);
        grid[gridPosition].Destroy();
        grid.Remove(gridPosition);
    }

    public List<Vector2> GetNeighbourTilePositions(Vector2 gridPosition, bool existing, NeighbourType neighbourType = NeighbourType.All, int maxNeighbourOffset = 1) {
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

        List<Vector2> neighbourTilePositions = FilterNonEmptyOrNonTiles(neighbourPositions);
        return neighbourTilePositions;
    }

    private static TileGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<TileGrid>();
        }
        return instance;
    }


}
