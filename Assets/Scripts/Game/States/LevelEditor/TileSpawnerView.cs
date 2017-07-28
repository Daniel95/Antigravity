using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerView : View, ITileSpawner {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    private int selectedTileIndex = 0;

    public override void Initialize() {
        tileSpawnerRef.Set(this);
    }

    public void SpawnTileAtScreenPosition(Vector2 screenPosition) {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        SpawnTileAtWorldPosition(worldPosition);
    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        Vector2 gridPosition = TileGrid.WorldToGridPosition(worldPosition);
        Vector2 tilePosition = TileGrid.GridToTilePosition(gridPosition);

        Tile tile = MakeWallTile(gridPosition);

        TileGrid.Grid[gridPosition] = tile;
    }

    private Tile MakeWallTile(Vector2 gridPosition) {
        Tile wallTile = GetTile(TileType.Wall, gridPosition, Vector2.zero);
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
        tileGameObject.transform.forward = direction;

        Tile tile = new Tile() {
            Type = tileType,
            GameObject = tileGameObject
        };
        return tile;
    }

    private void Awake() {
        float tileWidth = TilePrefabTypeContainer.Instance.GetPrefabByTileType(TileType.Wall).transform.localScale.x;
        TileGrid.SetTileSize(tileWidth);    
    }

}