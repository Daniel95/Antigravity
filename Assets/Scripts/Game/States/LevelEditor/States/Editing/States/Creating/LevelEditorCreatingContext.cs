﻿using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

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
            .Do<DispatchLevelEditorTouchDownOnGridPositionEventCommand>()
            .Do<LevelEditorStartSelectionFieldAtScreenPositionCommand>()
            .Do<AbortIfSelectionFieldIsSameAsPreviousSelectionFieldCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<SwipeMovedEvent>()
            .Do<DispatchLevelEditorSwipeMovedToGridPositionEventCommand>();

        On<LevelEditorSwipeMovedToGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionIsTheSameAsSelectedGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedGridPositionStatusCommand>()
            .Do<DebugLogMessageCommand>("Moved")
            .Do<DispatchLevelEditorSwipeMovedToNewGridPositionEventCommand>();

        On<TouchUpEvent>()
            .Do<DispatchLevelEditorTouchUpOnGridPositionEventCommand>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<SwipeEndEvent>()
            .Do<LevelEditorClearSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>()
            .Do<ShowBoxOverlayCommand>(false);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<LevelEditorUpdateSelectionFieldToSwipePositionCommand>()
            .Do<DispatchLevelEditorSelectionFieldChangedEventCommand>();

        On<LevelEditorSelectionFieldTileSpawnLimitReachedEvent>()
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

    }

}
