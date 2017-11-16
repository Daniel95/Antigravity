using IoCPlus;

public class LevelEditorLevelObjectTranslateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()    
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Rotate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

        On<LevelEditorSwipeStartOnWorldEvent>()
            .Do<LevelEditorSaveSelectedLevelObjectPreviousTransformValuesCommand>()
            .Do<LevelEditorUpdateTranslateStartPositionStatusCommand>();

        On<LevelEditorSwipeMovedOnWorldEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorMoveSelectedLevelObjectToWorldPositionCommand>();

        On<LevelEditorSwipeMovedOnWorldEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithTilesCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectDoesNotCollideWithTilesCommand>()
            .Do<LevelEditorResetSelectedLevelObjectStatusCommand>();

        On<LevelEditorSwipeMovedOnWorldEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithLevelObjectsCommand>()
            .Do<AbortIfLevelEditorSelectedLevelObjectDoesNotCollideWithLevelObjectsCommand>()
            .Do<LevelEditorResetSelectedLevelObjectStatusCommand>();

        On<LevelEditorSwipeMovedOnWorldEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorSaveSelectedLevelObjectPreviousTransformValuesCommand>();

        On<LevelEditorTranslateStartWorldPositionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateTranslateStartOffsetPositionCommand>();

    }

}