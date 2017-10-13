using IoCPlus;
using UnityEngine;

public class LevelEditorOnGridLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectSectionStatus>>();

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectSectionStatus>>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorTouchDownOnOnGridLevelObjectEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectSectionTransformTypeCommand>();

        On<PinchStartedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfLevelEditorGridPositionDoesContainElementCommand>()
            .Do<LevelEditorInstantiateLevelObjectAtGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);


    }

}