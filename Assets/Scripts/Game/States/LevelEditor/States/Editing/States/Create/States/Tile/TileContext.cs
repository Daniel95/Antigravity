using IoCPlus;

public class TileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<SelectionFieldStatusView>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            //.Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GridSnapSizeButtonUI", CanvasLayer.UI)
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginSizeToMinusHalfSnapSizeCommand>()
            .Do<SetGridOverlayStepToTileSnapSizeCommand>()
            .Do<SetSelectionFieldEnabledCommand>(true)
            .GotoState<BuildingTileContext>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<SelectionFieldStatusView>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GoToLevelObjectStateButtonUI", CanvasLayer.UI)
            //.Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/GridSnapSizeButtonUI", CanvasLayer.UI)
            .Do<ClearSelectionFieldCommand>()
            .Do<ShowGridOverlayCommand>(false)
            .Do<SetSelectionFieldEnabledCommand>(false);

        OnChild<ErasingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.BuildingTile)
            .GotoState<BuildingTileContext>();

        OnChild<BuildingTileContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.ErasingTile)
            .GotoState<ErasingTileContext>();

        On<TouchDownOnLevelObjectEvent>()
            .Do<AbortIfMousePositionContainsTileCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.LevelObject)
            .Do<UpdateSelectedLevelObjectCommand>();

        On<TouchDownOnTileEvent>()
            .Do<StartSelectionFieldAtGridPositionCommand>();

        On<TouchStartOnGridPositionEvent>()
            .Do<AbortIfSelectionFieldIsDisabledCommand>()
            .Do<StartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchSelectionFieldChangedEventCommand>();

        On<SwipeEndEvent>()
            .Dispatch<SpawningTilesStoppedEvent>()
            .Do<ClearSelectionFieldCommand>()
            .Do<ClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<PinchStartedEvent>()
            .Do<SetSelectionFieldEnabledCommand>(false);

        On<PinchMovedEvent>()
            .Do<RemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<ClearSelectionFieldAvailableGridPositionsCommand>();

        On<PinchStoppedEvent>()
            .Do<ClearSelectionFieldCommand>()
            .Do<SetSelectionFieldEnabledCommand>(true);

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<UpdateSelectedGridPositionStatusCommand>()
            .Do<DispatchSwipeMovedToNewGridPositionEventCommand>();

        On<SwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfSelectionFieldIsDisabledCommand>()
            .Do<UpdateSelectionFieldToSwipePositionCommand>()
            .Do<DispatchSelectionFieldChangedEventCommand>();

        On<TouchUpOnGridPositionEvent>()
            .Dispatch<SpawningTilesStoppedEvent>()
            .Do<ClearSelectionFieldCommand>()
            .Do<ClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<SelectionFieldTileSpawnLimitReachedEvent>()
            .Do<SetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Error);

        On<SelectionFieldChangedEvent>()
            .Do<SetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Default);

        On<SelectionFieldChangedEvent>()
            .Do<AbortIfBoxOverlayIsShownCommand>()
            .Do<ShowBoxOverlayCommand>(true);

        On<SelectionFieldChangedEvent>()
            .Do<UpdateBoxOverlayToSelectionFieldCommand>();

        On<SelectionFieldEnabledUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsEnabledCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<CameraZoomedEvent>()
            .Do<AbortIfGridOverlayIsNotShownCommand>()
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>();

        On<GridSnapSizeStatusUpdatedEvent>()
            .Do<SetGridOverlayStepToTileSnapSizeCommand>();

    }

}