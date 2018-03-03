using IoCPlus;

public class LevelObjectScaleContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Translate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Rotate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<SwipeMovedOnWorldEvent>()
            .Do<LevelObjectScaleCommand>();

    }

}