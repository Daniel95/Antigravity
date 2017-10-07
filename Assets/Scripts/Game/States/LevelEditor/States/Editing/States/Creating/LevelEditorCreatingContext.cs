using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .Do<EnableCameraZoomInputCommand>(true)
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginToHalfTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>()
            .GotoState<LevelEditorTileContext>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false)
            .Do<ShowGridOverlayCommand>(false)
            .Do<EnableCameraZoomInputCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorStateEvent>()
        .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelObjects)
        .GotoState<LevelEditorLevelObjectContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Tile)
            .GotoState<LevelEditorTileContext>();

        On<OutsideUITouchStartEvent>()
            .Do<DispatchLevelEditorTouchDownOnGridPositionEventCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsDisabledCommand>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<SwipeMovedEvent>()
            .Do<DispatchLevelEditorSwipeMovedToGridPositionEventCommand>();

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedGridPositionStatusCommand>()
            .Do<DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand>();

        On<TouchUpEvent>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<PinchStartedEvent>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false);

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

        On<PinchStoppedEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsDisabledCommand>()
            .Do<LevelEditorUpdateSelectionFieldToSwipePositionCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<LevelEditorSelectionFieldTileSpawnLimitReachedEvent>()
            .Do<LevelEditorSetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Error);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorSetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Default);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<AbortIfBoxOverlayIsShownCommand>()
            .Do<ShowBoxOverlayCommand>(true);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorUpdateBoxOverlayToSelectionFieldCommand>();

        On<LevelEditorSelectionFieldEnabledUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsEnabledCommand>()
            .Do<ShowBoxOverlayCommand>(false);

    }

}
