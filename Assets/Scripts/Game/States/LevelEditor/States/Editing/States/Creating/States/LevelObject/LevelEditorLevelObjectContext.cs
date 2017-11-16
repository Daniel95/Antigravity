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
            .Do<LevelEditorInstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI")
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(true);

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectStatus>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorResetSelectedLevelObjectStatusCommand>()
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(false);

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
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfLevelEditorScreenPositionDoesContainGridElementCommand>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand>()
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(true)
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

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

        On<CollisionEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotLevelObjectCommand>()
            .Do<LevelEditorAddColliderToSelectedLevelObjectCollisionCollidersCommand>()
            .Do<DispatchLevelEditorLevelObjectCollisionEnter2DEventCommand>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotLevelObjectCommand>()
            .Do<LevelEditorAddColliderToSelectedLevelObjectCollisionCollidersCommand>()
            .Do<DispatchLevelEditorLevelObjectTriggerEnter2DEventCommand>();

        On<CollisionExit2DEvent>()
            .Do<AbortIfGameObjectIsNotLevelObjectCommand>()
            .Do<LevelEditorRemoveColliderFromSelectedLevelObjectCollisionCollidersCommand>();

        On<TriggerExit2DEvent>()
            .Do<AbortIfGameObjectIsNotLevelObjectCommand>()
            .Do<LevelEditorRemoveColliderFromSelectedLevelObjectCollisionCollidersCommand>();

    }

}