using IoCPlus;

public class LevelEditorObjectsContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(false)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Objects/GoToTilesStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<LevelEditorSetSelectionFieldEnabledCommand>(true)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Objects/GoToTilesStateButtonUI", CanvasLayer.UI);


    }

}