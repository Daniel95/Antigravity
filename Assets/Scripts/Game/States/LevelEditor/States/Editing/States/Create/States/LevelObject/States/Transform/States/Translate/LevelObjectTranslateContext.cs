using IoCPlus;

public class LevelObjectTranslateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()    
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Rotate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToScaleButtonUI", CanvasLayer.UI);

        On<SwipeStartOnWorldEvent>()
            .Do<UpdateTranslateStartPositionStatusCommand>()
            .Do<UpdateTranslateStartOffsetPositionCommand>();

        On<SwipeMovedOnWorldEvent>()
            .Do<TranslateSelectedLevelObjectCommand>();

    }

}