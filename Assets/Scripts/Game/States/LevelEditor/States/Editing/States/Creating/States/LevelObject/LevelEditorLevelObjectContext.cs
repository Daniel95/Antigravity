﻿using IoCPlus;
using UnityEngine;

public class LevelEditorLevelObjectContext : Context {


    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectNodeViewStatus>>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectInputTypeStatus>>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectSectionStatus>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorInstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI");

        On<LeaveContextSignal>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectNodeViewStatus>>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectInputTypeStatus>>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectSectionStatus>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorLevelObjectButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectInputTypeToLevelObjectNodeInputTypeCommand>();

        On<LevelEditorLevelObjectInputTypeButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectInputTypeStatusCommand>();

        On<LevelEditorSelectedLevelObjectInputTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectInputTypeIsNotInputTypeCommand>(LevelObjectInputType.Translate)
            .GotoState<LevelEditorLevelObjectTranslateContext>();

        On<LevelEditorSelectedLevelObjectInputTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectInputTypeIsNotInputTypeCommand>(LevelObjectInputType.Scale)
            .GotoState<LevelEditorLevelObjectScaleContext>();

        On<LevelEditorSelectedLevelObjectInputTypeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectInputTypeIsNotInputTypeCommand>(LevelObjectInputType.Rotate)
            .GotoState<LevelEditorLevelObjectRotateContext>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfLevelEditorGridPositionIsOccupiedCommand>()
            .Do<LevelEditorInstantiateLevelObjectAtGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainLevelObjectSectionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectedLevelObjectSectionStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectedLevelObjectNodeStatusUpdatedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/SetLevelObjectInputTypeToTranslateButtonUI", CanvasLayer.UI);

    }

}