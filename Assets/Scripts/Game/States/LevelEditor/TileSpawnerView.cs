using IoCPlus;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerView : View, ITileSpawner {

    [Inject] private Ref<ITileSpawner> tileSpawnerRef;

    [SerializeField] private List<Tile> tiles = new List<Tile>();

    private int selectedTileIndex = 0;

    public override void Initialize() {
        tileSpawnerRef.Set(this);
    }

    public void SpawnTileAtScreenPosition(Vector2 screenPosition) {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        SpawnTileAtWorldPosition(worldPosition);
    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        Vector2 gridPosition = LevelEditorGrid.GetGridPosition(worldPosition);
        Tile tile = tiles[selectedTileIndex];

        LevelEditorGrid.Grid[gridPosition] = tile.Type;
        GameObject prefab = tile.Prefab;
        Instantiate(prefab, gridPosition, new Quaternion());
    }

}