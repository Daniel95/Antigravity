using IoCPlus;

public class LevelEditorTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Tile/GoToObjectsStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Tile/GoToObjectsStateButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorReplaceNewTilesInSelectionFieldCommand>();

        On<LevelEditorTouchUpOnGridPositionEvent>()
            .Do<LevelEditorClearSelectionFieldAvailableTileGridPositionsCommand>();

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldAvailableTileGridPositionsCommand>();

       On<PinchStartedEvent>()
            .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableTileGridPositionsCommand>();

        On<PinchMovedEvent>()
            .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableTileGridPositionsCommand>();

    }

}