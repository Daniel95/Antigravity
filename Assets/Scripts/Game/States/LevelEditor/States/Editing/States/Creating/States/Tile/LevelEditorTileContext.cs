using IoCPlus;

public class LevelEditorTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            .GotoState<LevelEditorBuildingTileContext>();

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI);

        OnChild<LevelEditorErasingTileContext, GoToLevelEditorStateEvent>()
        .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.BuildingTile)
        .GotoState<LevelEditorBuildingTileContext>();

        OnChild<LevelEditorBuildingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.ErasingTile)
            .GotoState<LevelEditorErasingTileContext>();

        On<LevelEditorTouchUpOnGridPositionEvent>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

        On<PinchStartedEvent>()
             .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
             .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

        On<PinchMovedEvent>()
            .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

    }

}