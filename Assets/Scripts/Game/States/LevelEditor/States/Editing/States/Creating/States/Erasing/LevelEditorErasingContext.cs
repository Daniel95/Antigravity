using IoCPlus;

public class LevelEditorErasingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Erasing/GoToBuildingStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Erasing/GoToBuildingStateButtonUI", CanvasLayer.UI);

        On<OnSelectionFieldUpdatedEvent>()
            .Do<RemoveTilesInSelectionFieldCommand>();

    }

}