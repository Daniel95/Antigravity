using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorTileInputView : View, ILevelEditorTileInput {

    [Inject] private LevelEditorSelectionFieldTileSpawnLimitReachedEvent selectionFieldTileSpawnLimitReachedEvent;

    public int SpawnLimit { get { return spawnLimit; } }

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    [SerializeField] private int spawnLimit = 100;

    private List<Vector2> selectionFieldAvailableGridPositions = new List<Vector2>();
    
    public override void Initialize() {
        levelEditorTileInputRef.Set(this);
    }

    public void ClearSelectionFieldAvailableGridPositions() {
        selectionFieldAvailableGridPositions.Clear();
    }

    public void RemoveTilesInSelectionField() {
        List<Vector2> previousSelectionFieldAvailableGridPositions = selectionFieldAvailableGridPositions;
        List<Vector2> nextSelectionFieldAvailableGridPositions = new List<Vector2>();

        List<Vector2> selectionFieldTileGridPositions = LevelEditorTileGrid.Instance.FilterNonEmptyOrNonTiles(LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions);

        foreach (Vector2 selectionFieldGridPosition in selectionFieldTileGridPositions) {
            if (!TileGenerator.CheckGridPositionEmptyOrNotUserGenerated(selectionFieldGridPosition) || CheckGridPositionPreviouslyOccupiedByLastSelectionField(selectionFieldGridPosition)) {
                nextSelectionFieldAvailableGridPositions.Add(selectionFieldGridPosition);
            }
        }

        List<Vector2> outdatedSelectionFieldAvailableGridPositions = previousSelectionFieldAvailableGridPositions.Except(nextSelectionFieldAvailableGridPositions).ToList();
        List<Vector2> newSelectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions.Except(previousSelectionFieldAvailableGridPositions).ToList();

        TileGenerator.SpawnTiles(outdatedSelectionFieldAvailableGridPositions);
        TileGenerator.RemoveTiles(newSelectionFieldAvailableGridPositions, true);

        selectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions;
    }

    public void RemoveTilesSpawnedByLastSelectionField() {
        TileGenerator.RemoveTiles(selectionFieldAvailableGridPositions, true);
    }

    public void SpawnTilesRemovedInLastSelectionField() {
        TileGenerator.SpawnTiles(selectionFieldAvailableGridPositions);
    }

    public void ReplaceNewTilesInSelectionField() {
        List<Vector2> previousSelectionFieldAvailableGridPositions = selectionFieldAvailableGridPositions;
        List<Vector2> nextSelectionFieldAvailableGridPositions = new List<Vector2>();

        List<Vector2> selectionFieldTileGridPositions = LevelEditorTileGrid.Instance.FilterNonEmptyOrNonTiles(LevelEditorSelectionFieldStatusView.SelectionFieldGridPositions);

        foreach (Vector2 selectionFieldGridPosition in selectionFieldTileGridPositions) {
            if (TileGenerator.CheckGridPositionEmptyOrNotUserGenerated(selectionFieldGridPosition) || CheckGridPositionPreviouslyOccupiedByLastSelectionField(selectionFieldGridPosition)) {
                nextSelectionFieldAvailableGridPositions.Add(selectionFieldGridPosition);
            }
        }

        List<Vector2> outdatedSelectionFieldAvailableGridPositions = previousSelectionFieldAvailableGridPositions.Except(nextSelectionFieldAvailableGridPositions).ToList();
        List<Vector2> newSelectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions.Except(previousSelectionFieldAvailableGridPositions).ToList();

        if(newSelectionFieldAvailableGridPositions.Count > spawnLimit) {
            selectionFieldTileSpawnLimitReachedEvent.Dispatch();
            return;
        }

        TileGenerator.RemoveTiles(outdatedSelectionFieldAvailableGridPositions, true, newSelectionFieldAvailableGridPositions);
        TileGenerator.SpawnTiles(newSelectionFieldAvailableGridPositions);

        selectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions;
    }

    private bool CheckGridPositionPreviouslyOccupiedByLastSelectionField(Vector2 gridPosition) {
        bool previouslyOccupiedByLastSelectionField = selectionFieldAvailableGridPositions.Contains(gridPosition);
        return previouslyOccupiedByLastSelectionField;
    }

    private Vector2 ConvertPositionToGridPosition(Vector2 position, LevelEditorInputType levelEditorInputType) {
        Vector2 gridPosition = new Vector2();

        switch (levelEditorInputType) {
            case LevelEditorInputType.ScreenSpace:
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(position);
                gridPosition = LevelEditorGridHelper.WorldToGridPosition(worldPosition);
                break;
            case LevelEditorInputType.WorldSpace:
                gridPosition = LevelEditorGridHelper.WorldToGridPosition(position);
                break;
            case LevelEditorInputType.GridSpace:
                gridPosition = position;
                Debug.LogWarning("Position is supposedly already a gridposition.");
                break;
        }

        return gridPosition;
    }

}