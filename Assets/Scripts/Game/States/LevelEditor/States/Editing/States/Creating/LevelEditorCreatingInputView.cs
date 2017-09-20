﻿using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorCreatingInputView : View, ILevelEditorCreatingInput {

    public List<Vector2> SelectionFieldGridPositions { get { return selectionFieldGridPositions; } }
    public List<Vector2> PreviousSelectionFieldGridPositions { get { return previousSelectionFieldGridPositions; } }

    [Inject] private Ref<ILevelEditorCreatingInput> levelEditorCreatingInputRef;

    private List<Vector2> selectionFieldGridPositions = new List<Vector2>();
    private List<Vector2> previousSelectionFieldGridPositions = new List<Vector2>();
    private List<Vector2> selectionFieldAvailableGridPositions = new List<Vector2>();
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

        previousSelectionFieldGridPositions = selectionFieldAvailableGridPositions;
        selectionFieldGridPositions = TileGrid.GetSelection(selectionFieldStartGridPosition, selectionFieldEndGridPosition);
    }

    public void ClearSelectionField() {
        selectionFieldAvailableGridPositions.Clear();
        previousSelectionFieldGridPositions = selectionFieldAvailableGridPositions;
        selectionFieldGridPositions.Clear();
    }

    public void RemoveTilesInSelectionField() {
        List<Vector2> gridPositionsToRemove = selectionFieldGridPositions.FindAll(x => TileGrid.ContainsPosition(x));
        RemoveTiles(gridPositionsToRemove, true);
    }

    public void ReplaceNewTilesInSelectionField() {
        List<Vector2> previousSelectionFieldAvailableGridPositions = selectionFieldAvailableGridPositions;
        List<Vector2> nextSelectionFieldAvailableGridPositions = new List<Vector2>();
        foreach (Vector2 selectionFieldGridPosition in selectionFieldGridPositions) {
            if (CheckGridPositionAvailability(selectionFieldGridPosition) || CheckGridPositionPreviouslyOccupiedByLastSelectionField(selectionFieldGridPosition)) {
                nextSelectionFieldAvailableGridPositions.Add(selectionFieldGridPosition);
            }
        }

        List<Vector2> outdatedSelectionFieldAvailableGridPositions = previousSelectionFieldAvailableGridPositions.Except(nextSelectionFieldAvailableGridPositions).ToList();
        List<Vector2> newSelectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions.Except(previousSelectionFieldAvailableGridPositions).ToList();

        RemoveTiles(outdatedSelectionFieldAvailableGridPositions, true, newSelectionFieldAvailableGridPositions);
        SpawnTiles(newSelectionFieldAvailableGridPositions);

        selectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions;
    }

    private bool CheckGridPositionAvailability(Vector2 gridPosition) {
        bool available = !TileGrid.ContainsPosition(gridPosition) || !TileGrid.GetTile(gridPosition).UserGenerated;
        return available;
    }

    private bool CheckGridPositionPreviouslyOccupiedByLastSelectionField(Vector2 gridPosition) {
        bool previouslyOccupiedByLastSelectionField = selectionFieldAvailableGridPositions.Contains(gridPosition);
        return previouslyOccupiedByLastSelectionField;
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

        List<Vector2> allNeighbourPositionsToRegenerate = TileGrid.GetNeighbourPositions(gridPosition, false);
        if(neighboursIgnoringRegenerate != null) {
            allNeighbourPositionsToRegenerate = allNeighbourPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        List<Vector2> nonUserGeneratedTilesToRegenerate = allNeighbourPositionsToRegenerate.FindAll(x => TileGrid.ContainsPosition(x) && !TileGrid.GetTile(x).UserGenerated);
        nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.RemoveTile(x));

        TileGenerator.Instance.GenerateTiles(allNeighbourPositionsToRegenerate);
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate) {
        gridPositions.ForEach(x => TileGrid.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = TileGrid.GetNeighbourPositions(gridPosition, false);
            allGridPositionsToRegenerate.Add(gridPosition);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => TileGrid.ContainsPosition(x) && !TileGrid.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.RemoveTile(x));

            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours) {
        gridPositions.ForEach(x => TileGrid.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = TileGrid.GetNeighbourPositions(gridPosition, false);
            allGridPositionsToRegenerate.Add(gridPosition);

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => TileGrid.ContainsPosition(x) && !TileGrid.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => TileGrid.RemoveTile(x));

            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    private void SpawnTile(Vector2 position, LevelEditorInputType levelEditorInputType, List<Vector2> neighboursIgnoringRegenerate = null) {
        Vector2 gridPosition = new Vector2();
        if(levelEditorInputType != LevelEditorInputType.GridSpace) {
            gridPosition = ConvertPositionToGridPosition(position, levelEditorInputType);
        }

        TileGrid.SetTile(gridPosition, new Tile() { UserGenerated = true });
        TileGenerator.Instance.RegenerateAutoGeneratedTiles();
        List<Vector2> positionsToGenerate = TileGrid.GetNeighbourPositions(gridPosition, false);

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
            List<Vector2> allNeighbourPositions = TileGrid.GetNeighbourPositions(gridPosition, false);
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