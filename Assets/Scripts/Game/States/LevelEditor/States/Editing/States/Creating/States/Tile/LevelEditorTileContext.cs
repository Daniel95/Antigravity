using IoCPlus;

public class LevelEditorTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GridSnapSizeButtonUI", CanvasLayer.UI)
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<LevelEditorSetGridOverlayOriginSizeToMinusHalfSnapSizeCommand>()
            .Do<LevelEditorSetGridOverlayStepToTileSnapSizeCommand>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .GotoState<LevelEditorBuildingTileContext>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectionFieldStatusView>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GridSnapSizeButtonUI", CanvasLayer.UI)
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<ShowGridOverlayCommand>(false)
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false);

        OnChild<LevelEditorErasingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.BuildingTile)
            .GotoState<LevelEditorBuildingTileContext>();

        OnChild<LevelEditorBuildingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.ErasingTile)
            .GotoState<LevelEditorErasingTileContext>();

        On<LevelEditorTouchStartOnGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsDisabledCommand>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

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
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

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