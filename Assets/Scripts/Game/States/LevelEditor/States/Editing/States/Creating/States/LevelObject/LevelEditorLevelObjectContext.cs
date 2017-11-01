using IoCPlus;
using UnityEngine;

public class LevelEditorLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelEditorLevelObjectTransformContext>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectStatus>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorInstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI");

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectStatus>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorResetSelectedLevelObjectStatusCommand>();

        On<TouchUpEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<PinchStoppedEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<LevelEditorLevelObjectButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand>();

        On<PinchStartedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<LevelEditorDestroySelectedLevelObjectCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfLevelEditorScreenPositionDoesContainGridElementCommand>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorTranslateStartWorldPositionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateTranslateStartOffsetPositionCommand>();

        On<LevelEditorLevelObjectTranslateEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorMoveSelectedLevelObjectToWorldPositionCommand>();

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroySelectedLevelObjectCommand>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand>();

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectedLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

    }

}