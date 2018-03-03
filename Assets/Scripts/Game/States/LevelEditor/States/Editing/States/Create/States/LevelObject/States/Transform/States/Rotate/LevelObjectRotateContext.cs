using IoCPlus;

public class LevelObjectRotateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Translate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

    }

}