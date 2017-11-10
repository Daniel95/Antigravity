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
            .Do<LevelEditorUpdateTranslateStartPositionStatusCommand>();

        On<LevelEditorSwipeMovedOnWorldEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorMoveSelectedLevelObjectToWorldPositionCommand>();

        On<LevelEditorTranslateStartWorldPositionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateTranslateStartOffsetPositionCommand>();

    }

}