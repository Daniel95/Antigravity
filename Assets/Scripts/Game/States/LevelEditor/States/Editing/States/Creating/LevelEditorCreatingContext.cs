using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorGridSnapSizeStatus>>()
            .Do<LevelEditorSetGridSnapSizeToGridSnapSizeTypeCommand>(GridSnapSizeType.Default)
            .GotoState<LevelEditorTileContext>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorGridSnapSizeStatus>>()
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
            .Do<DispatchLevelEditorTouchStartOnWorldEventCommand>()
            .Do<DispatchLevelEditorTouchStartOnGridPositionEventCommand>();

        On<TouchDownEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchDownOnWorldEventCommand>();

        On<TouchUpEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorTouchUpOnWorldEventCommand>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>();

        On<SwipeStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorSwipeStartOnWorldEventCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchLevelEditorSwipeMovedOnWorldEvent>()
            .Do<DispatchLevelEditorSwipeMovedToGridPositionEventCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfLevelEditorScreenPositionDoesContainGridElementCommand>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfMousePositionIsNotOverLevelObjectCommand>()
            .Dispatch<LevelEditorTouchDownOnLevelObjectEvent>();

        On<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorLevelObjectContext>>()
            .GotoState<LevelEditorLevelObjectContext>();

        On<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectStatusCommand>();

        On<LevelEditorTouchStartOnGridPositionEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorTileContext>>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainTileCommand>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedGridPositionStatusCommand>()
            .Do<DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand>();

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

        On<LevelEditorGridSnapSizeStatusUpdatedEvent>()
            .Do<LevelEditorSetGridOverlayStepToTileSnapSizeCommand>();

    }

}
