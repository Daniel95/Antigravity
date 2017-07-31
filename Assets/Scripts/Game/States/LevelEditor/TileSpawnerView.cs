using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerView : View, ITileSpawner {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    private List<Vector2> selectionFieldPositions = new List<Vector2>();
    private Vector2 selectionFieldStartGridPosition;

    public override void Initialize() {
        tileSpawnerRef.Set(this);
    }

    public void SpawnTileAtScreenPosition(Vector2 screenPosition) {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        SpawnTileAtWorldPosition(worldPosition);
    }

    public void StartSelectionField(Vector2 selectionFieldStartScreenPosition) {
        Vector2 selectionFieldStartWorldPosition = Camera.main.ScreenToWorldPoint(selectionFieldStartScreenPosition);
        selectionFieldStartGridPosition = TileGrid.WorldToGridPosition(selectionFieldStartWorldPosition);
        UpdateSelectionField(selectionFieldStartScreenPosition);
    }

    public void UpdateSelectionField(Vector2 selectionFieldEndScreenPosition) {
        Vector2 selectionFieldEndWorldScreenPosition = Camera.main.ScreenToWorldPoint(selectionFieldEndScreenPosition);
        Vector2 selectionFieldEndGridPosition = TileGrid.WorldToGridPosition(selectionFieldEndWorldScreenPosition);

        selectionFieldPositions.ForEach(x => DestroyTileAtGridPosition(x));

        selectionFieldPositions = TileGrid.GetSelection(selectionFieldStartGridPosition, selectionFieldEndGridPosition);

        selectionFieldPositions.ForEach(x => SpawnTileAtGridPosition(x));
    }

    public void FinishSelectionField() {

    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        Vector2 gridPosition = TileGrid.WorldToGridPosition(worldPosition);
        SpawnTileAtGridPosition(gridPosition);
    }

    private void SpawnTileAtGridPosition(Vector2 gridPosition) {
        Tile tile = GenerateTile(gridPosition);

        if (TileGrid.Grid.ContainsKey(gridPosition)) {
            TileGrid.Grid[gridPosition] = tile;
        } else {
            TileGrid.Grid.Add(gridPosition, tile);
        }

        //UpdateNeighbourTiles(gridPosition);
    }

    private void DestroyTileAtGridPosition(Vector2 gridPosition) {
        if (!TileGrid.Grid.ContainsKey(gridPosition)) { return; }

        TileGrid.Grid[gridPosition].Destroy();
        TileGrid.Grid.Remove(gridPosition);
    }

    private void UpdateNeighbourTiles(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition);

        foreach (Vector2 neighbourPosition in neighbourPositions) {
            if(TileGrid.Grid.ContainsKey(neighbourPosition)) { continue; }
            Vector2 cornerDirection;
            if(CheckForCorner(neighbourPosition, out cornerDirection)) {
                TileGrid.Grid.Add(neighbourPosition, MakeConvexCorner(neighbourPosition, -cornerDirection));
            }
        }
    }

    private Tile GenerateTile(Vector2 gridPosition) {
        List<Vector2> neighourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition, true);
        List<Vector2> directNeighbourPositions = TileGrid.GetGridPositionDirectNeighbourPositions(gridPosition, neighourPositions);

        if (neighourPositions.Count <= 0 || directNeighbourPositions.Count <= 0) { return MakeSoloTile(gridPosition); }
        if(directNeighbourPositions.Count >= 3) { return MakeDefaultTile(gridPosition); }

        List<Vector2> horizontalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.x == x.x);
        List<Vector2> verticalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.y == x.y);

        Tile tile;

        if (horizontalNeighbourPositions.Count == 1 && verticalNeighbourPositions.Count == 1) {
            float xDirectionToNeighbour = horizontalNeighbourPositions[0].x - gridPosition.x;
            float yDirectionToNeighbour = verticalNeighbourPositions[0].y - gridPosition.y;
            Vector2 directionToNeighbours = new Vector2(xDirectionToNeighbour, yDirectionToNeighbour);

            tile = MakeConvexCorner(gridPosition, -directionToNeighbours);
        } else {
            Vector2 directionToNeighbour = directNeighbourPositions[0] - gridPosition;
            tile = MakeEnding(gridPosition, -directionToNeighbour);
        }

        return tile;
    }

    private bool CheckForCorner(Vector2 gridPosition, out Vector2 cornerDirection) {
        cornerDirection = Vector2.zero;

        if (TileGrid.Grid.ContainsKey(gridPosition)) { return false; }

        List<Vector2> neighourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition, true);
        List<Vector2> directNeighbourPositions = TileGrid.GetGridPositionDirectNeighbourPositions(gridPosition, neighourPositions);

        List<Vector2> horizontalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.x == x.x);
        List<Vector2> verticalNeighbourPositions = directNeighbourPositions.FindAll(x => gridPosition.y == x.y);

        if (horizontalNeighbourPositions.Count != 1 || verticalNeighbourPositions.Count != 1) { return false; }

        float xDirectionToNeighbour = horizontalNeighbourPositions[0].x - gridPosition.x;
        float yDirectionToNeighbour = verticalNeighbourPositions[0].y - gridPosition.y;
        Vector2 directionToNeighbours = new Vector2(xDirectionToNeighbour, yDirectionToNeighbour);
        cornerDirection = directionToNeighbours;

        return true;
    }

    private Tile MakeDefaultTile(Vector2 gridPosition) {
        Tile wallTile = GetTile(TileType.Default, gridPosition, Vector2.zero);
        return wallTile;
    }

    private Tile MakeSoloTile(Vector2 gridPosition) {
        Tile wallTile = GetTile(TileType.Solo, gridPosition, Vector2.zero);
        return wallTile;
    }

    private Tile MakeConvexCorner(Vector2 gridPosition, Vector2 direction) {
        Tile wallTile = GetTile(TileType.ConvexCorner, gridPosition, direction);
        return wallTile;
    }

    private Tile MakeConcaveCorner(Vector2 gridPosition, Vector2 direction) {
        Tile wallTile = GetTile(TileType.ConcaveCorner, gridPosition, direction);
        return wallTile;
    }

    private Tile MakeEnding(Vector2 gridPosition, Vector2 direction) {
        Tile wallTile = GetTile(TileType.Ending, gridPosition, direction);
        return wallTile;
    }

    private Tile GetTile(TileType tileType, Vector2 gridPosition, Vector2 direction) {
        GameObject prefab = TilePrefabTypeContainer.Instance.GetPrefabByTileType(tileType);
        Vector2 tilePosition = TileGrid.GridToTilePosition(gridPosition);

        GameObject tileGameObject = Instantiate(prefab, tilePosition, new Quaternion());
        //tileGameObject.transform.forward = direction;

        Tile tile = new Tile() {
            Type = tileType,
            GameObject = tileGameObject
        };
        return tile;
    }

    private void Awake() {
        float tileWidth = TilePrefabTypeContainer.Instance.GetPrefabByTileType(TileType.Default).transform.localScale.x;
        TileGrid.SetTileSize(tileWidth);    
    }

}