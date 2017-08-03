﻿using IoCPlus;
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

        List<Vector2> newSelectionFieldGridPositions = TileGrid.GetSelection(selectionFieldStartGridPosition, selectionFieldEndGridPosition);

        selectionFieldGridPositions.ForEach(x => DestroyTileAtGridPosition(x));
        selectionFieldGridPositions.ForEach(x => {
            List<Vector2> neighbourPositions = TileGrid.GetNeighbourPositions(x, false, NeighbourType.All, 1);
            UpdateTileGridPositions(neighbourPositions, newSelectionFieldGridPositions);
        });

        selectionFieldGridPositions = newSelectionFieldGridPositions;
        SpawnTileAtGridPositions(selectionFieldGridPositions);
    }

    public void FinishSelectionField() {
        selectionFieldGridPositions.Clear();
    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        Vector2 gridPosition = TileGrid.WorldToGridPosition(worldPosition);
        TileGrid.SetTile(gridPosition, new Tile() { UserGenerated = true });
        TileGenerator.Instance.RegenerateAutoGeneratedTiles();
        SpawnTileAtGridPosition(gridPosition);
    }

    private void SpawnTileAtGridPositions(List<Vector2> tileGridPositions) {
        tileGridPositions.ForEach(x => TileGrid.SetTile(x, new Tile() { UserGenerated = true }));
        TileGenerator.Instance.RegenerateAutoGeneratedTiles();
        tileGridPositions.ForEach(x => SpawnTileAtGridPosition(x));
    }

    private void SpawnTileAtGridPosition(Vector2 gridPosition) {
        List<Vector2> neighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);
        UpdateTileGridPositions(neighbourPositions, selectionFieldGridPositions);

        Tile tile = TileGenerator.Instance.GenerateTile(gridPosition);
        if(tile.TileType == TileType.Empty) { return; }

        TileGrid.SetTile(gridPosition, tile);
    }

    private void DestroyTileAtGridPosition(Vector2 gridPosition) {
        if (!TileGrid.ContainsPosition(gridPosition)) { return; }
        TileGrid.RemoveTile(gridPosition);
    }

    private void UpdateTileGridPositions(List<Vector2> gridPositions, List<Vector2> excludedGridPositions = null) {
        foreach (Vector2 gridPosition in gridPositions) {
            bool gridPositionIsExcluded = excludedGridPositions != null && excludedGridPositions.Contains(gridPosition);
            if (gridPositionIsExcluded) { continue; }

            Tile tile = TileGenerator.Instance.GenerateTile(gridPosition);
            if (tile.TileType == TileType.Empty) { continue; }

            TileGrid.SetTile(gridPosition, tile);
        }
    }

}