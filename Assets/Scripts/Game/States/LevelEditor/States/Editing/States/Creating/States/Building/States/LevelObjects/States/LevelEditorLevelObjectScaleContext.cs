using IoCPlus;

public class LevelEditorLevelObjectScaleContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Translate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToRotateButtonUI", CanvasLayer.UI);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<DebugLogMessageCommand>("Scale");

    }

}