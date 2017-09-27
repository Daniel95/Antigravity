using IoCPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorTilesView : View, ILevelEditorTiles {

    [Inject] private LevelEditorSelectionFieldSpawnLimitReachedEvent levelEditorSelectionFieldSpawnLimitReachedEvent;

    public int SpawnLimit { get { return spawnLimit; } }

    [Inject] private LevelEditorSelectionFieldStatus selectionFieldStatus;

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    [SerializeField] private int spawnLimit = 100;

    private List<Vector2> selectionFieldAvailableGridPositions = new List<Vector2>();

    public override void Initialize() {
        levelEditorTilesRef.Set(this);
    }

    public void ClearSelectionFieldAvailableGridPositions() {
        selectionFieldAvailableGridPositions.Clear();
    }

    public void RemoveTilesInSelectionField() {
        List<Vector2> previousSelectionFieldAvailableGridPositions = selectionFieldAvailableGridPositions;
        List<Vector2> nextSelectionFieldAvailableGridPositions = new List<Vector2>();
        foreach (Vector2 selectionFieldGridPosition in selectionFieldStatus.SelectionFieldGridPositions) {
            if (!CheckGridPositionEmptyOrNotUserGenerated(selectionFieldGridPosition) || CheckGridPositionPreviouslyOccupiedByLastSelectionField(selectionFieldGridPosition)) {
                nextSelectionFieldAvailableGridPositions.Add(selectionFieldGridPosition);
            }
        }

        List<Vector2> outdatedSelectionFieldAvailableGridPositions = previousSelectionFieldAvailableGridPositions.Except(nextSelectionFieldAvailableGridPositions).ToList();
        List<Vector2> newSelectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions.Except(previousSelectionFieldAvailableGridPositions).ToList();

        SpawnTiles(outdatedSelectionFieldAvailableGridPositions);
        RemoveTiles(newSelectionFieldAvailableGridPositions, true);

        selectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions;
    }

    public void ReplaceNewTilesInSelectionField() {
        List<Vector2> previousSelectionFieldAvailableGridPositions = selectionFieldAvailableGridPositions;
        List<Vector2> nextSelectionFieldAvailableGridPositions = new List<Vector2>();
        foreach (Vector2 selectionFieldGridPosition in selectionFieldStatus.SelectionFieldGridPositions) {
            if (CheckGridPositionEmptyOrNotUserGenerated(selectionFieldGridPosition) || CheckGridPositionPreviouslyOccupiedByLastSelectionField(selectionFieldGridPosition)) {
                nextSelectionFieldAvailableGridPositions.Add(selectionFieldGridPosition);
            }
        }

        List<Vector2> outdatedSelectionFieldAvailableGridPositions = previousSelectionFieldAvailableGridPositions.Except(nextSelectionFieldAvailableGridPositions).ToList();
        List<Vector2> newSelectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions.Except(previousSelectionFieldAvailableGridPositions).ToList();

        if(newSelectionFieldAvailableGridPositions.Count > spawnLimit) {
            levelEditorSelectionFieldSpawnLimitReachedEvent.Dispatch();
            return;
        }

        RemoveTiles(outdatedSelectionFieldAvailableGridPositions, true, newSelectionFieldAvailableGridPositions);
        SpawnTiles(newSelectionFieldAvailableGridPositions);

        selectionFieldAvailableGridPositions = nextSelectionFieldAvailableGridPositions;
    }

    private bool CheckGridPositionEmptyOrNotUserGenerated(Vector2 gridPosition) {
        bool available = !LevelEditorTileGrid.Instance.Contains(gridPosition) || !LevelEditorTileGrid.Instance.GetTile(gridPosition).UserGenerated;
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
        LevelEditorTileGrid.Instance.RemoveTile(gridPosition);

        if (!regenerateNeighbours) { return; }

        List<Vector2> allNeighbourPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, false);
        if(neighboursIgnoringRegenerate != null) {
            allNeighbourPositionsToRegenerate = allNeighbourPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        List<Vector2> nonUserGeneratedTilesToRegenerate = allNeighbourPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.Contains(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
        nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        TileGenerator.Instance.GenerateTiles(allNeighbourPositionsToRegenerate);
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours, List<Vector2> neighboursIgnoringRegenerate) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(neighboursIgnoringRegenerate).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.Contains(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    private void RemoveTiles(List<Vector2> gridPositions, bool regenerateNeighbours) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        if (!regenerateNeighbours) { return; }

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allGridPositionsToRegenerate = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, false);
            allGridPositionsToRegenerate = allGridPositionsToRegenerate.Except(gridPositions).ToList();
            allGridPositionsToRegenerate.Add(gridPosition);

            List<Vector2> nonUserGeneratedTilesToRegenerate = allGridPositionsToRegenerate.FindAll(x => LevelEditorTileGrid.Instance.Contains(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            TileGenerator.Instance.GenerateTiles(allGridPositionsToRegenerate);
        }
    }

    private void SpawnTile(Vector2 position, LevelEditorInputType levelEditorInputType, List<Vector2> neighboursIgnoringRegenerate = null) {
        Vector2 gridPosition = new Vector2();
        if(levelEditorInputType != LevelEditorInputType.GridSpace) {
            gridPosition = ConvertPositionToGridPosition(position, levelEditorInputType);
        }

        LevelEditorTileGrid.Instance.SetTile(gridPosition, new Tile() { UserGenerated = true });

        List<Vector2> positionsToGenerate = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, false);

        if(neighboursIgnoringRegenerate != null) {
            positionsToGenerate = positionsToGenerate.Except(neighboursIgnoringRegenerate).ToList();
        }

        List<Vector2> nonUserGeneratedTilesToRegenerate = positionsToGenerate.FindAll(x => LevelEditorTileGrid.Instance.Contains(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
        nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

        positionsToGenerate.Add(gridPosition);
        TileGenerator.Instance.GenerateTiles(positionsToGenerate);
    }

    private void SpawnTiles(List<Vector2> gridPositions) {
        gridPositions.ForEach(x => LevelEditorTileGrid.Instance.SetTile(x, new Tile() { UserGenerated = true }));

        foreach (Vector2 gridPosition in gridPositions) {
            List<Vector2> allNeighbourPositions = LevelEditorTileGrid.Instance.GetNeighbourPositions(gridPosition, false);
            List<Vector2> positionsToGenerate = allNeighbourPositions.Except(gridPositions).ToList();

            List<Vector2> nonUserGeneratedTilesToRegenerate = positionsToGenerate.FindAll(x => LevelEditorTileGrid.Instance.Contains(x) && !LevelEditorTileGrid.Instance.GetTile(x).UserGenerated);
            nonUserGeneratedTilesToRegenerate.ForEach(x => LevelEditorTileGrid.Instance.RemoveTile(x));

            positionsToGenerate.Add(gridPosition);
            TileGenerator.Instance.GenerateTiles(positionsToGenerate);
        }
    }

    private Vector2 ConvertPositionToGridPosition(Vector2 position, LevelEditorInputType levelEditorInputType) {
        float nodeSize = LevelEditorGridNodeSize.Instance.Size;
        Vector2 gridPosition = new Vector2();

        switch (levelEditorInputType) {
            case LevelEditorInputType.ScreenSpace:
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(position);
                gridPosition = GridHelper.WorldToGridPosition(worldPosition, nodeSize);
                break;
            case LevelEditorInputType.WorldSpace:
                gridPosition = GridHelper.WorldToGridPosition(position, nodeSize);
                break;
            case LevelEditorInputType.GridSpace:
                gridPosition = position;
                Debug.LogWarning("Position is supposedly already a gridposition.");
                break;
        }

        return gridPosition;
    }

}