using IoCPlus;
using UnityEngine;

public class LevelEditorOffGridLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<TouchDownEvent>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<LevelEditorInstantiateAndSelectOffGridLevelObjectAtScreenPositionCommand>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorLevelObjectTranslateOffGridEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<LevelEditorMoveSelectedOffGridLevelObjectToWorldPositionCommand>();

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroySelectedOffGridLevelObjectCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>()
            .Do<LevelEditorResetSelectedOffGridLevelObjectStatusCommand>();

        On<LevelEditorSelectedOffGridLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedOffGridLevelObjectCommand>();

        On<LevelEditorSelectedOffGridLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectedOffGridLevelObjectStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

    }

}