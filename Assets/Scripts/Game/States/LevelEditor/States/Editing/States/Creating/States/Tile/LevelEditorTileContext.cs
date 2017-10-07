using IoCPlus;

public class LevelEditorTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .GotoState<LevelEditorBuildingTileContext>();

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false);

        OnChild<LevelEditorErasingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.BuildingTile)
            .GotoState<LevelEditorBuildingTileContext>();

        OnChild<LevelEditorBuildingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.ErasingTile)
            .GotoState<LevelEditorErasingTileContext>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsDisabledCommand>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<TouchUpEvent>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<PinchStartedEvent>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false);

        On<PinchMovedEvent>()
            .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

        On<PinchStoppedEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsDisabledCommand>()
            .Do<LevelEditorUpdateSelectionFieldToSwipePositionCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<LevelEditorTouchUpOnGridPositionEvent>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

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