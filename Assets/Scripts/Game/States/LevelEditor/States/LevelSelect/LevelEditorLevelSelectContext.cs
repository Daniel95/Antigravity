using IoCPlus;

public class LevelEditorLevelSelectContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/GoToMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelEditorLevelSelectGridLayoutGroup", CanvasLayer.UI)
            .Do<InstantiateLevelEditorLevelSelectButtonsInGridLayoutGroupCommand>();

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/GoToMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelEditorLevelSelectGridLayoutGroup", CanvasLayer.UI);

        On<LevelEditorLevelSelectButtonClickedEvent>()
            .Do<LevelEditorUpdateLevelNameStatusCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.Editing);

    }

}