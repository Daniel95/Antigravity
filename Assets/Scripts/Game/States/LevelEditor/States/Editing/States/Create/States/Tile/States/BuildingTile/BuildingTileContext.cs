using IoCPlus;

public class BuildingTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/BuildingTile/GoToErasingTileStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/BuildingTile/GoToErasingTileStateButtonUI", CanvasLayer.UI);

        On<SelectionFieldChangedEvent>()
            .Do<ReplaceNewTilesInSelectionFieldCommand>();

        On<SpawningTilesStoppedEvent>()
            .Do<CalculateTileGridVolumeRectanglesCommand>()
            .Do<DestroyLevelObjectsWithCollisionInSelectionFieldCommand>();

        On<PinchStartedEvent>()
            .Do<RemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<ClearSelectionFieldAvailableGridPositionsCommand>();

    }

}