using IoCPlus;

public class LevelEditorOffGridLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<PinchStartedEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<LevelEditorDestroySelectedOffGridLevelObjectCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfMousePositionIsOverOffGridLevelObjectCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<LevelEditorInstantiateAndSelectOffGridLevelObjectAtScreenPositionCommand>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorTranslateStartWorldPositionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateOffGridTranslateStartOffsetPositionCommand>();

        On<LevelEditorLevelObjectTranslateOffGridEvent>()
            .Do<AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand>()
            .Do<LevelEditorMoveSelectedOffGridLevelObjectToWorldPositionCommand>();

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroySelectedOffGridLevelObjectCommand>();

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