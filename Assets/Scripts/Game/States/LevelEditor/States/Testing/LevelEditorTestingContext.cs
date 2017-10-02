using IoCPlus;

public class LevelEditorTestingContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI)
            .Do<LevelEditorLoadLevelSaveDataCommand>()
            .Do<LevelEditorCombineStandardTilesCommand>()
            .GotoState<LevelContext>();

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/Testing/GoToLevelEditorLevelSelectStateButtonUI", CanvasLayer.UI);

    }
}
