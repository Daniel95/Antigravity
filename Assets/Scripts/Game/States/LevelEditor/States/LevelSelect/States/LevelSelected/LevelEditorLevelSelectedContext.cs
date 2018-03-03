using IoCPlus;

public class LevelEditorLevelSelectedContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorLevelOverviewStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorTestingStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorEditingStateButtonUI", CanvasLayer.UI);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorLevelOverviewStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorTestingStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelSelected/GoToLevelEditorEditingStateButtonUI", CanvasLayer.UI);

        On<GoToLevelEditorLevelOverviewStateButtonClickedEvent>()
            .Do<ClearLevelNameStatusCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.LevelsOverview);

    }

}