﻿using IoCPlus;

public class LevelEditorCreatingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<OnSelectionFieldUpdatedEvent>();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI)
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayOriginToHalfTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>()
            .GotoState<LevelEditorBuildingContext>();

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Creating/GoToNavigatingStateButtonUI", CanvasLayer.UI);

        OnChild<LevelEditorErasingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Building)
            .GotoState<LevelEditorBuildingContext>();

        OnChild<LevelEditorBuildingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Erasing)
            .GotoState<LevelEditorErasingContext>();

        On<OutsideUITouchStartEvent>()
            .Do<StartSelectionFieldAtPositionCommand>()
            .Dispatch<OnSelectionFieldUpdatedEvent>();

        On<SwipeMovedEvent>()
            .Do<UpdateSelectionFieldToSwipePositionCommand>()
            .Dispatch<OnSelectionFieldUpdatedEvent>();

        On<TouchUpEvent>()
            .Do<ClearSelectionFieldCommand>();

        On<SwipeEndEvent>()
            .Do<ClearSelectionFieldCommand>();

    }

}
