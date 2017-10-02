using IoCPlus;

public class LevelEditorLevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelEditorLevelSelectGridLayoutGroup", CanvasLayer.UI)
            .Do<LevelEditorInstantiateLevelSelectButtonsCommand>()
            .GotoState<LevelEditorLevelOverViewContext>();

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelEditorLevelSelectGridLayoutGroup", CanvasLayer.UI);

        OnChild<LevelEditorLevelSelectedContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelOverView)
            .GotoState<LevelEditorLevelOverViewContext>();

        OnChild<LevelEditorLevelOverViewContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelSelected)
            .GotoState<LevelEditorLevelSelectedContext>();

    }

}