﻿using IoCPlus;

public class LevelEditorLevelObjectScaleContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Translate)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<EnterContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand>(LevelObjectTransformType.Scale)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToTranslateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectTransformTypeToRotateButtonUI", CanvasLayer.UI);

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<DebugLogMessageCommand>("Scale");

    }

}