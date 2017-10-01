using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelEditorTileGrid : LevelEditorGridPositions {

    public static LevelEditorTileGrid Instance { get { return GetInstance(); } }

    public Dictionary<Vector2, Tile> Grid { get { return tileGrid; } }

    private static LevelEditorTileGrid instance;

    private Dictionary<Vector2, Tile> tileGrid = new Dictionary<Vector2, Tile>();

    public Tile GetTile(Vector2 gridPosition) {
        return tileGrid[gridPosition];
    }

    public List<Vector2> GetGridPositionsByTileType(TileType tileType) {
        List<Vector2> gridPositionsWithType = tileGrid.Keys.ToList().FindAll(x => tileGrid[x].TileType == tileType);
        return gridPositionsWithType;
    }

    public void SetTile(Vector2 gridPosition, Tile tile) {
        if (Contains(gridPosition)) {
            RemoveTile(gridPosition);
        }
        AddTile(gridPosition, tile);
    }

    public void UpdateTile(Vector2 gridPosition, Tile tile) {
        Tile oldTile = tileGrid[gridPosition];
        oldTile.Destroy();
        tileGrid[gridPosition] = tile;
    }

    public void AddTile(Vector2 gridPosition, Tile tile) {
        Add(gridPosition);
        tileGrid.Add(gridPosition, tile);
    }

    public override void Clear() {
        base.Clear();
        foreach (Vector2 gridPosition in tileGrid.Keys) {
            tileGrid[gridPosition].Destroy();
        }
        tileGrid.Clear();
    }

    public bool ContainsTile(Vector2 gridPositions) {
        bool contains = tileGrid.ContainsKey(gridPositions);
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
        tileGrid[gridPosition].Destroy();
        tileGrid.Remove(gridPosition);
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

    private static LevelEditorTileGrid GetInstance() {
        if(instance == null) {
            instance = FindObjectOfType<LevelEditorTileGrid>();
        }
        return instance;
    }

}
