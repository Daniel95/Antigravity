using IoCPlus;

public class LevelEditorTestingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI)
            .Do<LevelEditorSetLevelEditorStatusCommand>(false)
            .Do<InstantiateLevelContainerCommand>()
            .Do<LevelEditorLoadLevelSaveDataCommand>()
            .Do<LevelEditorCombineStandardTilesCommand>()
            .GotoState<LevelContext>();

        On<LeaveContextSignal>()
            .Do<LevelEditorSetLevelEditorStatusCommand>(true)
            .Do<LevelEditorClearGridCommand>()
            .Do<LevelEditorClearLevelNameStatusCommand>()
            .Do<DestroyLevelContainerCommand>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI);

    }
}
