using IoCPlus;

public class LevelEditorLevelOverViewContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelOverview/GoToLevelEditorMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<DispatchSetlevelSelectButtonInteractableEventCommand>(true);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelOverview/GoToLevelEditorMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<DispatchSetlevelSelectButtonInteractableEventCommand>(false);

        On<LevelSelectButtonClickedEvent>()
            .Do<UpdateLevelNameStatusCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.LevelSelected);

    }

}