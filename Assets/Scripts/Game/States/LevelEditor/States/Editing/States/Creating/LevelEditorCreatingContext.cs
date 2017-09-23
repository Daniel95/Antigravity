using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<LevelEditorSelectionFieldChangedEvent>();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginToHalfTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>()
            .GotoState<LevelEditorBuildingContext>();

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        OnChild<LevelEditorErasingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Building)
            .GotoState<LevelEditorBuildingContext>();

        OnChild<LevelEditorBuildingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Erasing)
            .GotoState<LevelEditorErasingContext>();

        On<OutsideUITouchStartEvent>()
            .Do<LevelEditorStartSelectionFieldAtScreenPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<SwipeMovedEvent>()
            .Do<LevelEditorUpdateSelectionFieldToSwipePositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<LevelEditorSelectionFieldSpawnLimitReachedEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsNotEnabledCommand>()
            .Do<LevelEditorSetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Error);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsNotEnabledCommand>()
            .Do<LevelEditorSetSelectionFieldBoxColorTypeCommand>(LevelEditorSelectionFieldBoxColorType.Default);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<AbortIfLevelEditorSelectionFieldIsNotEnabledCommand>()
            .Do<AbortIfBoxOverlayIsShownCommand>()
            .Do<ShowBoxOverlayCommand>(true);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorUpdateBoxOverlayToSelectionFieldCommand>();

        On<TouchUpEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

    }

}
