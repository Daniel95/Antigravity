using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<ShowGridOverlayCommand>(true)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginToHalfTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false)
            .Do<EnableCameraZoomInputCommand>(false)
            .Do<EnableCameraMoveInputCommand>(false)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelObject)
            .GotoState<LevelEditorLevelObjectContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Tile)
            .GotoState<LevelEditorTileContext>();

        On<OutsideUITouchStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchDownOnGridPositionEventCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorSwipeMovedToGridPositionEventCommand>();

        On<TouchUpEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainLevelObjectSectionCommand>()
            .Dispatch<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<DispatchLevelEditorTouchDownOnOnGridLevelObjectEventCommand>();

        On<TouchDownEvent>()
            .Do<AbortIfLevelEditorMousePositionDoesContainLevelObjectSectionCommand>()
            .Do<AbortIfMousePositionIsNotOverOffGridLevelObjectCommand>()
            .Dispatch<LevelEditorTouchDownOnLevelObjectEvent>()
            .Dispatch<LevelEditorTouchDownOnOffGridLevelObjectEvent>();

        On<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorLevelObjectContext>>()
            .GotoState<LevelEditorLevelObjectContext>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainTileCommand>()
            .Do<AbortIfContextStateIsCommand<LevelEditorTileContext>>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedGridPositionStatusCommand>()
            .Do<DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand>();

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

    }

}
