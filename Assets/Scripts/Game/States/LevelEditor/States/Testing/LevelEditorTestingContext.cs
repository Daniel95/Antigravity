using IoCPlus;

public class LevelEditorTestingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<DeserializedLevelSaveDataStatus>();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI)
            .Do<UpdateDeserializedLevelSaveDataCommand>()
            .Do<InstantiateLevelContainerCommand>()
            .Do<SpawnCombinedStandardTilesCommand>()
            .Do<SpawnNonStandardTilesCommand>()
            .Do<SpawnLevelObjectsCommand>()
            .GotoState<LevelContext>();

        On<LeaveContextSignal>()
            .Do<LevelEditorClearGridCommand>()
            .Do<LevelEditorClearLevelNameStatusCommand>()
            .Do<DestroyLevelContainerCommand>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI);

    }
}
