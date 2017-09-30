using IoCPlus;

public class LevelEditorLevelObjectsContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/GoToTilesStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/GoToTilesStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectEditorNodeTypeIsCommand>(LevelObjectType.None)
            .Do<AbortIfLevelEditorGridPositionIsOccupiedCommand>()
            .Do<LevelEditorInstantiateLevelObjectAtGridPositionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionCommand>();

        On<LevelEditorTouchDownOnGridPositionEvent>()
            .Do<AbortIfLevelEditorGridPositionDoesNotContainLevelObjectSectionCommand>()
            .Do<LevelEditorUpdateSelectedLevelObjectSectionCommand>();

        On<LevelEditorSwipeMovedToNewGridPositionEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand>()
            .Do<LevelEditorMoveSelectedLevelObjectToGridPositionCommand>();

        On<LevelEditorTouchUpOnGridPositionEvent>()
            .Do<LevelEditorResetSelectedLevelObjectSectionStatusCommand>();

    }

}