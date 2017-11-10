using IoCPlus;

public class LevelEditorBuildingTileContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/BuildingTile/GoToErasingTileStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Editing/Creating/Tile/BuildingTile/GoToErasingTileStateButtonUI", CanvasLayer.UI);

        On<LevelEditorSelectionFieldChangedEvent>()
            .Do<LevelEditorReplaceNewTilesInSelectionFieldCommand>();

        On<LevelEditorSpawningTilesStoppedEvent>()
            .Do<LevelEditorDestroyCollisionWithGridLevelObjectsInSelectionFieldCommand>();

        On<PinchStartedEvent>()
            .Do<LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand>()
            .Do<LevelEditorClearSelectionFieldAvailableGridPositionsCommand>();

    }

}