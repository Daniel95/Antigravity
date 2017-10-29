using IoCPlus;

public class LevelEditorOnGridLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<LevelEditorSetGridSnapSizeToGridSnapSizeTypeCommand>(GridSnapSizeType.Smallest)
            .Do<ShowGridOverlayCommand>(true);

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false)
            .Do<LevelEditorSetGridSnapSizeToPreviousCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeToLevelObjectSectionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectSectionCommand>();

        On<PinchStartedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorTouchStartOnGridPositionEvent>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfLevelEditorGridPositionDoesContainElementCommand>()
            .Do<LevelEditorInstantiateOnGridLevelObjectAtGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorLevelObjectTranslateOnGridEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<LevelEditorMoveSelectedOnGridLevelObjectToGridPositionCommand>();

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand>();

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