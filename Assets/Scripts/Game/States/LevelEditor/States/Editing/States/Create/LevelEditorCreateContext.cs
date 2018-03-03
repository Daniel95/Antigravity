using IoCPlus;

public class LevelEditorCreateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<EnableCameraMoveInputCommand>(true)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe2Fingers)
            .Do<AddStatusViewToStatusViewContainerCommand<GridSnapSizeStatus>>()
            .Do<SetGridSnapSizeToGridSnapSizeTypeCommand>(GridSnapSizeType.Default)
            .GotoState<TileContext>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<GridSnapSizeStatus>>()
            .Do<EnableCameraZoomInputCommand>(false)
            .Do<EnableCameraMoveInputCommand>(false)
            .Do<SetCameraMoveInputTypeCommand>(CameraMoveInputType.Swipe);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelObject)
            .GotoState<LevelObjectContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Tile)
            .GotoState<TileContext>();

        On<OutsideUITouchStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchTouchStartOnWorldEventCommand>()
            .Do<DispatchTouchStartOnGridPositionEventCommand>();

        On<TouchDownEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchTouchDownOnWorldEventCommand>();

        On<TouchUpEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchTouchUpOnWorldEventCommand>()
            .Do<DispatchTouchUpOnGridPositionEventCommand>();

        On<SwipeStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchSwipeStartOnWorldEventCommand>();

        On<SwipeMovedEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<DispatchSwipeMovedOnWorldEvent>()
            .Do<DispatchSwipeMovedToGridPositionEventCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfMousePositionIsNotOverLevelObjectCommand>()
            .Dispatch<TouchDownOnLevelObjectEvent>();

        On<TouchStartOnGridPositionEvent>()
            .Do<AbortIfGridPositionDoesNotContainTileCommand>()
            .Do<DispatchTouchDownOnTileEventCommand>();

    }

}
