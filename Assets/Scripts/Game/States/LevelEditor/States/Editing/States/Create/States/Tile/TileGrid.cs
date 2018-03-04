using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileGrid : LevelEditorGrid {

    public static TileGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, Tile> TilesByGridPosition { get { return tilesByGridPosition; } }

    private static TileGrid instance;

    private Dictionary<Vector2, Tile> tilesByGridPosition = new Dictionary<Vector2, Tile>();

    public Tile GetTile(Vector2 gridPosition) {
        return tilesByGridPosition[gridPosition];
    }

    public List<Vector2> GetTileGridPositions() {
        List<Vector2> tileGridPositions = tilesByGridPosition.Keys.ToList();
        return tileGridPositions;
    }

    public List<Vector2> GetUserGeneratedTileGridPositions() {
        List<Vector2> userGeneratedTileGridPosition = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Tile> tileByGridPosition in tilesByGridPosition) {
            if(tileByGridPosition.Value.UserGenerated) {
                userGeneratedTileGridPosition.Add(tileByGridPosition.Key);
            }
        }

        return userGeneratedTileGridPosition;
    }

    public List<Vector2> GetNonUserGeneratedTileGridPositions() {
        List<Vector2> userGeneratedTileGridPosition = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Tile> tileByGridPosition in tilesByGridPosition) {
            if (!tileByGridPosition.Value.UserGenerated) {
                userGeneratedTileGridPosition.Add(tileByGridPosition.Key);
            }
        }

        return userGeneratedTileGridPosition;
    }

    public List<Vector2> GetGridPositionsByTileType(TileType tileType) {
        List<Vector2> gridPositionsWithType = tilesByGridPosition.Keys.ToList().FindAll(x => tilesByGridPosition[x].TileType == tileType);
        return gridPositionsWithType;
    }

    public void SetTile(Vector2 gridPosition, Tile tile) {
        if (Contains(gridPosition)) {
            RemoveTile(gridPosition);
        }
        AddTile(gridPosition, tile);
    }

    public void UpdateTile(Vector2 gridPosition, Tile tile) {
        Tile oldTile = tilesByGridPosition[gridPosition];
        oldTile.Destroy();
        tilesByGridPosition[gridPosition] = tile;
    }

    public void AddTile(Vector2 gridPosition, Tile tile) {
        Add(gridPosition);
        tilesByGridPosition.Add(gridPosition, tile);
    }

    public override void Clear() {
        base.Clear();
        foreach (Vector2 gridPosition in tilesByGridPosition.Keys) {
            tilesByGridPosition[gridPosition].Destroy();
        }
        tilesByGridPosition.Clear();
    }

    public bool ContainsTile(Vector2 gridPositions) {
        bool contains = tilesByGridPosition.ContainsKey(gridPositions);
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
        tilesByGridPosition[gridPosition].Destroy();
        tilesByGridPosition.Remove(gridPosition);
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
