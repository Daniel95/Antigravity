﻿using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorCreatingInputView : View, ILevelEditorCreatingInput {

    public List<Vector2> CurrentSelectionFieldGridPositions { get { return currentSelectionFieldGridPositions; } }
    public List<Vector2> PreviousSelectionFieldGridPositions { get { return previousSelectionFieldGridPositions; } }

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    private List<Vector2> currentSelectionFieldGridPositions = new List<Vector2>();
    private List<Vector2> previousSelectionFieldGridPositions = new List<Vector2>();
    private Vector2 selectionFieldStartGridPosition;

    public override void Initialize() {
        levelEditorCreatingInputRef.Set(this);
    }

    public void StartSelectionField(Vector2 selectionFieldStartScreenPosition) {
        Vector2 selectionFieldStartWorldPosition = Camera.main.ScreenToWorldPoint(selectionFieldStartScreenPosition);
        selectionFieldStartGridPosition = TileGrid.WorldToGridPosition(selectionFieldStartWorldPosition);
        UpdateSelectionField(selectionFieldStartScreenPosition);
    }

    public void UpdateSelectionField(Vector2 selectionFieldEndScreenPosition) {
        Vector2 selectionFieldEndWorldScreenPosition = Camera.main.ScreenToWorldPoint(selectionFieldEndScreenPosition);
        Vector2 selectionFieldEndGridPosition = TileGrid.WorldToGridPosition(selectionFieldEndWorldScreenPosition);

        previousSelectionFieldGridPositions = currentSelectionFieldGridPositions;
        currentSelectionFieldGridPositions = TileGrid.GetSelection(selectionFieldStartGridPosition, selectionFieldEndGridPosition);
    }

    public void ClearSelectionField() {
        currentSelectionFieldGridPositions.Clear();
    }

    public void RemoveTilesInSelectionField() {
        List<Vector2> gridPositionsToRemove = currentSelectionFieldGridPositions.FindAll(x => TileGrid.ContainsPosition(x));
        RemoveTiles(gridPositionsToRemove, true);
    }

    public void ReplaceNewTilesInSelectionField() {
        RemoveTiles(previousSelectionFieldGridPositions, true, currentSelectionFieldGridPositions);
        SpawnTiles(currentSelectionFieldGridPositions);
    }

    public void SpawnTileAtScreenPosition(Vector2 screenPosition) {
        SpawnTile(screenPosition, LevelEditorInputType.ScreenSpace);
    }

    public void SpawnTileAtWorldPosition(Vector2 worldPosition) {
        SpawnTile(worldPosition, LevelEditorInputType.WorldSpace);
    }

    public void SpawnTileAtGridPosition(Vector2 gridPosition) {
        SpawnTile(gridPosition, LevelEditorInputType.GridSpace);
    }

    private void RemoveTile(Vector2 gridPosition, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate = null) {
        TileGrid.RemoveTile(gridPosition);

        if (!regenerateNeighbours) { return; }

        List<Vector2> allNeighbourPositionsToRegenrate = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);
        if(neighboursIgnoringRegenerate != null) {
            allNeighbourPositionsToRegenrate = allNeighbourPositionsToRegenrate.Except(neighboursIgnoringRegenerate).ToList();
        }
        TileGenerator.Instance.GenerateTiles(allNeighbourPositionsToRegenrate);
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate) {
        gridPositions.ForEach(x => TileGrid.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenrate = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);
            allGridPositionsToRegenrate.Add(gridPosition);
            allGridPositionsToRegenrate = allGridPositionsToRegenrate.Except(neighboursIgnoringRegenerate).ToList();
            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenrate);
        }
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours) {
        gridPositions.ForEach(x => TileGrid.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenrate = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);
            allGridPositionsToRegenrate.Add(gridPosition);
            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenrate);
        }
    }

    private void SpawnTile(Vector2 position, LevelEditorInputType levelEditorInputType, List<Vector2> neighboursIgnoringRegenerate = null) {
        Vector2 gridPosition = new Vector2();
        if(levelEditorInputType != LevelEditorInputType.GridSpace) {
            gridPosition = ConvertPositionToGridPosition(position, levelEditorInputType);
        }

        TileGrid.SetTile(gridPosition, new Tile() { UserGenerated = true });
        TileGenerator.Instance.RegenerateAutoGeneratedTiles();
        List<Vector2> positionsToGenerate = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);

        if(neighboursIgnoringRegenerate != null) {
            positionsToGenerate = positionsToGenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        positionsToGenerate.Add(gridPosition);
        TileGenerator.Instance.GenerateTiles(positionsToGenerate);
    }

    private void SpawnTiles(List<Vector2> gridPositions) {
        gridPositions.ForEach(x => TileGrid.SetTile(x, new Tile() { UserGenerated = true }));
        TileGenerator.Instance.RegenerateAutoGeneratedTiles();

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, false, NeighbourType.All, 1);
            List<Vector2> positionsToGenerate = allNeighbourPositions.Except(gridPositions).ToList();
            positionsToGenerate.Add(gridPosition);
            TileGenerator.Instance.GenerateTiles(positionsToGenerate);
        }
    }

    private Vector2 ConvertPositionToGridPosition(Vector2 position, LevelEditorInputType levelEditorInputType) {
        Vector2 gridPosition = new Vector2();

        switch (levelEditorInputType) {
            case LevelEditorInputType.ScreenSpace:
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(position);
                gridPosition = TileGrid.WorldToGridPosition(worldPosition);
                break;
            case LevelEditorInputType.WorldSpace:
                gridPosition = TileGrid.WorldToGridPosition(position);
                break;
            case LevelEditorInputType.GridSpace:
                gridPosition = position;
                Debug.LogWarning("Position is supposedly already a gridposition.");
                break;
        }

        return gridPosition;
    }

}