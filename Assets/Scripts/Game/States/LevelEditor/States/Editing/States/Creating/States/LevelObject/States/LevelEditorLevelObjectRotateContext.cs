using IoCPlus;

public class LevelEditorLevelObjectRotateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Translate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeInputTypeDoesNotContainInputTypeCommand>(LevelObjectInputType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectInputTypeToScaleButtonUI", CanvasLayer.UI);

    }

}