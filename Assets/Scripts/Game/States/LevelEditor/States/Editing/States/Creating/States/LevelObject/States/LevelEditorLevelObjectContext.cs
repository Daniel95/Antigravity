﻿using IoCPlus;
using UnityEngine;

public class LevelEditorLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelEditorLevelObjectTransformContext>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectNodeViewStatus>>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorInstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI");

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectNodeViewStatus>>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<LevelEditorTouchDownOnOnGridLevelObjectEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorOnGridLevelObjectContext>>()
            .GotoState<LevelEditorOnGridLevelObjectContext>();

        On<LevelEditorTouchDownOnOnGridLevelObjectEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorTouchDownOnOffGridLevelObjectEvent>()
            .Do<AbortIfContextStateIsCommand<LevelEditorOffGridLevelObjectContext>>()
            .GotoState<LevelEditorOffGridLevelObjectContext>();

        On<LevelEditorTouchDownOnOffGridLevelObjectEvent>()
            .Do<LevelEditorUpdateSelectedOffGridLevelObjectStatusCommand>();

        On<LevelEditorTouchUpOnGridPositionEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<PinchStoppedEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<LevelEditorLevelObjectButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand>();

        On<LevelEditorSelectedLevelObjectNodeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNotOnGridCommand>()
            .GotoState<LevelEditorOnGridLevelObjectContext>();

        On<LevelEditorSelectedLevelObjectNodeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsOnGridCommand>()
            .GotoState<LevelEditorOffGridLevelObjectContext>();

    }

}