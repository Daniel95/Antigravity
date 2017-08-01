using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerView : View, ITileSpawner {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    private List<Vector2> selectionFieldGridPositions = new List<Vector2>();
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

        selectionFieldGridPositions.ForEach(x => DestroyTileAtGridPosition(x));

        selectionFieldGridPositions = TileGrid.GetSelection(selectionFieldStartGridPosition, selectionFieldEndGridPosition);
        SpawnTileAtGridPositions(selectionFieldGridPositions);
    }

    public void FinishSelectionField() {
        selectionFieldGridPositions.Clear();
    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        Vector2 gridPosition = TileGrid.WorldToGridPosition(worldPosition);
        SpawnTileAtGridPosition(gridPosition);
    }

    private void SpawnTileAtGridPositions(List<Vector2> tileGridPositions) {
        tileGridPositions.ForEach(x => TileGrid.SetTile(x, new Tile()));
        tileGridPositions.ForEach(x => SpawnTileAtGridPosition(x));
    }

    private void SpawnTileAtGridPosition(Vector2 gridPosition) {
        Tile tile = GenerateTile(gridPosition);

        TileGrid.SetTile(gridPosition, tile);

        CheckForObsoleteConcaveCorners();

        List<Vector2> neighbourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition);
        UpdateGridPositions(neighbourPositions, selectionFieldGridPositions);
    }

    private void DestroyTileAtGridPosition(Vector2 gridPosition) {
        if (!TileGrid.ContainsPosition(gridPosition)) { return; }
        TileGrid.RemoveTile(gridPosition);
    }

    private void UpdateGridPositions(List<Vector2> gridPositions, List<Vector2> excludedGridPositions = null) {
        foreach (Vector2 gridPosition in gridPositions) {
            bool gridPositionIsExcluded = excludedGridPositions != null && excludedGridPositions.Contains(gridPosition);
            if (gridPositionIsExcluded) { continue; }

            if(TileGrid.ContainsPosition(gridPosition)) {
                Tile tile = GenerateTile(gridPosition);
                TileGrid.UpdateTile(gridPosition, tile);
            } else {
                Vector2 cornerDirection;
                if(CheckForCorner(gridPosition, out cornerDirection)) {
                    Tile tile = MakeConvexCorner(gridPosition, -cornerDirection);
                    TileGrid.AddTile(gridPosition, tile);
                }
            }
        }
    }

    private void CheckForObsoleteConcaveCorners() {
        List<Vector2> concavePositions = TileGrid.GetGridPositionsByTileType(TileType.ConcaveCorner);
        foreach (Vector2 concavePosition in concavePositions) {
            Vector2 cornerDirection;
            if (!CheckForCorner(concavePosition, out cornerDirection)) {
                TileGrid.RemoveTile(concavePosition);
            }
        }
    }

    //tiles are not being spawned on empty grid spaces since we are not checking it.
    private Tile GenerateTile(Vector2 gridPosition) {
        List<Vector2> neighourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition, 1, true);
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

        if (TileGrid.ContainsPosition(gridPosition)) { return false; }

        List<Vector2> neighourPositions = TileGrid.GetGridPositionNeighbourPositions(gridPosition, 1, true);
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