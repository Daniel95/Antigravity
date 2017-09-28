using IoCPlus;

public class LevelEditorLevelObjectsContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<LevelEditorSelectedLevelObjectStatus>();

        On<EnterContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/GoToTilesStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/GoToTilesStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/LevelObjects/LevelObjectButtonGridLayoutGroupUI", CanvasLayer.UI);

        On<TouchDownEvent>()


        On<TouchUpEvent>()
            .Do<AbortIfLevelEditorSelectedLevelObjectIsNoneCommand>()
            .Do<InstantiateSelectedLevelObjectAtScreenPositionCommand>();

    }

}