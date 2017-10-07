using IoCPlus;

public class LevelEditorErasingTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/ErasingTile/GoToBuildingTileStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/ErasingTile/GoToBuildingTileStateButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorRemoveTilesInSelectionFieldCommand>();

    }

}