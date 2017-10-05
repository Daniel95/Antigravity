public interface ILevelEditorTileInput {

    int SpawnLimit { get; }

    void ClearSelectionFieldAvailableGridPositions();
    void RemoveTilesInSelectionField();
    void RemoveTilesSpawnedByLastSelectionField();
    void ReplaceNewTilesInSelectionField();

}
