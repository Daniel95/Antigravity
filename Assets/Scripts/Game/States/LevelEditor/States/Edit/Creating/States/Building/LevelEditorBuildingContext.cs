using IoCPlus;

public class LevelEditorBuildingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Edit/Creating/Building/GoToErasingStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Edit/Creating/Building/GoToErasingStateButtonUI", CanvasLayer.UI);

        On<OnSelectionFieldUpdatedEvent>()
            .Do<ReplaceNewTilesInSelectionFieldCommand>();

    }

}