using IoCPlus;
using UnityEngine;

public class LevelObjectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelObjectTransformContext>()
            .Do<AddStatusViewToStatusViewContainerCommand<SelectedLevelObjectTransformTypeStatusView>>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<InstantiateLevelObjectButtonsCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonUI")
            .Do<EnableLevelObjectZonesStandardColorEditorAlphaCommand>(true);

        On<LeaveContextSignal>()
            .Do<ResetSelectedLevelObjectTransformTypeStatusCommand>()
            .Do<RemoveStatusViewFromStatusViewContainerCommand<SelectedLevelObjectTransformTypeStatusView>>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/GoToTileStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI)
            .Do<EnableLevelObjectZonesStandardColorEditorAlphaCommand>(false);

        On<LeaveContextSignal>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<UnselectSelectedLevelObjectCommand>();

        On<TouchDownOnTileEvent>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<StartSelectionFieldAtGridPositionCommand>()
            .Do<AbortIfContextStateIsCommand<TileContext>>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.Tile);

        On<TouchDownOnLevelObjectEvent>()
            .Do<UpdateSelectedLevelObjectCommand>();

        On<TouchUpEvent>()
            .Do<SetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<PinchStoppedEvent>()
            .Do<SetReleasedSinceLevelObjectSpawnStatusCommand>(true);

        On<LevelObjectButtonClickedEvent>()
            .Do<UpdateSelectedLevelObjectNodeCommand>()
            .Do<UpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand>();

        On<PinchStartedEvent>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfReleasedSinceLevelObjectSpawnStatusIsCommand>(true)
            .Do<DestroySelectedLevelObjectCommand>();

        On<TouchStartEvent>()
            .Do<AbortIfSelectedLevelObjectNodeIsNullCommand>()
            .Do<AbortIfTouchStarted2FingersAfterIdleCommand>()
            .Do<AbortIfReleasedSinceLevelObjectSpawnStatusIsCommand>(false)
            .Do<AbortIfScreenPositionContainsGridElementCommand>()
            .Do<AbortIfMousePositionIsOverLevelObjectCommand>()
            .Do<LevelEditorInstantiateAndSelectLevelObjectAtScreenPositionCommand>()
            .Do<EnableLevelObjectZonesStandardColorEditorAlphaCommand>(true)
            .Do<SetReleasedSinceLevelObjectSpawnStatusCommand>(false);

        On<LevelObjectDeleteButtonClickedEvent>()
            .Do<DestroySelectedLevelObjectCommand>();

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<UpdateSelectedLevelObjectNodeToSelectedLevelObjectStatusCommand>()
            .Do<UpdateSelectedLevelObjectTransformTypeToSelectedLevelObjectCommand>();

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

        On<LoadLevelSaveDataCommand>()
            .Do<AbortIfSelectedLevelObjectIsNotNullCommand>()
            .Do<AbortIfChildInCanvasLayerDoesNotExistCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/LevelObject/DestroyLevelObjectButtonUI", CanvasLayer.UI);

    }

}