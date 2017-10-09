using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<ShowGridOverlayCommand>(true)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginToHalfTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false)
            .Do<EnableCameraZoomInputCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelObject)
            .GotoState<LevelEditorLevelObjectContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Tile)
            .GotoState<LevelEditorTileContext>();

        On<OutsideUITouchStartEvent>()
            .Do<AbortIfMultiTouchedAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchDownOnGridPositionEventCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfMultiTouchedAfterIdleCommand>()
            .Do<DispatchLevelEditorSwipeMovedToGridPositionEventCommand>();

        On<TouchUpEvent>()
            .Do<AbortIfMultiTouchedAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainLevelObjectSectionCommand>()
            .Do<DispatchLevelEditorTouchDownOnLevelObjectEventCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainTileCommand>()
            .Do<AbortIfContextStateIsCommand<LevelEditorTileContext>>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorLevelObjectContext>>()
            .GotoState<LevelEditorLevelObjectContext>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedGridPositionStatusCommand>()
            .Do<DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand>();

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

    }

}
