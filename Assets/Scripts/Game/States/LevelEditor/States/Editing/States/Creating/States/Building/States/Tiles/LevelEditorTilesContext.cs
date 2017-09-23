using IoCPlus;

public class LevelEditorTilesContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Tiles/GoToObjectsStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Building/Tiles/GoToObjectsStateButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<ReplaceNewTilesInSelectionFieldCommand>();

    }

}