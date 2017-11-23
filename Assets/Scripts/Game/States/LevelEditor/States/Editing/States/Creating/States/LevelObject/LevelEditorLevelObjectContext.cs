using IoCPlus;
using UnityEngine;

public class LevelEditorLevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelEditorLevelObjectTransformContext>()
            .Do<AddStatusViewToStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorInstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI")
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(true);

        On<LeaveContextSignal>()
            .Do<LevelEditorResetSelectedLevelObjectTransformTypeStatusCommand>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<LevelEditorSelectedLevelObjectTransformTypeStatus>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(false);

        On<LeaveContextSignal>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUnselectSelectedLevelObjectCommand>();

        On<LevelEditorTouchDownOnTileEvent>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<LevelEditorStartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfContextStateIsCommand<LevelEditorTileContext>>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.Tile);

        On<LevelEditorTouchDownOnLevelObjectEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectCommand>();

        On<TouchUpEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<PinchStoppedEvent>()
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<LevelEditorLevelObjectButtonClickedEvent>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand>();

        On<PinchStartedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<LevelEditorDestroySelectedLevelObjectCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfLevelEditorReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfLevelEditorScreenPositionDoesContainGridElementCommand>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand>()
            .Do<LevelEditorEnableLevelObjectZonesStandardColorEditorAlphaCommand>(true)
            .Do<LevelEditorSetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelEditorLevelObjectDeleteButtonClickedEvent>()
            .Do<LevelEditorDestroySelectedLevelObjectCommand>();

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand>();

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LevelEditorNewLevelObjectSelectedEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

    }

}