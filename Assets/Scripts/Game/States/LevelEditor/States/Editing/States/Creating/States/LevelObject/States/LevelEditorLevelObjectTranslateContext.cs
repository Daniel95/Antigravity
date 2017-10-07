using IoCPlus;

public class LevelEditorLevelObjectTranslateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Rotate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<LevelEditorMoveSelectedLevelObjectToGridPositionCommand>();

    }

}