public interface ILevelEditorTiles {

    int SpawnLimit { get; }

    void ClearSelectionFieldAvailableGridPositions();
    void RemoveTilesInSelectionField();
    void ReplaceNewTilesInSelectionField();

}
