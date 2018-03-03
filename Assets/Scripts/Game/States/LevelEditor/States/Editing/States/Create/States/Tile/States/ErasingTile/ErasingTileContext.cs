using IoCPlus;

public class ErasingTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/ErasingTile/GoToBuildingTileStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/ErasingTile/GoToBuildingTileStateButtonUI", CanvasLayer.UI);

        On<SelectionFieldChangedEvent>()
            .Do<RemoveTilesInSelectionFieldCommand>();

        On<PinchStartedEvent>()
            .Do<SpawnTilesRemovedInLastSelectionFieldCommand>()
            .Do<ClearSelectionFieldAvailableGridPositionsCommand>();

    }

}