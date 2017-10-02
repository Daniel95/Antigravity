using IoCPlus;

public class LevelEditorLevelOverViewContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<InstantiateViewInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelOverview/GoToLevelEditorMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<DispatchLevelEditorSetlevelSelectButtonInteractableEventCommand>(true);

        On<LeaveContextSignal>()
            .Do<DestroyChildInCanvasLayerCommand>("UI/LevelEditor/LevelSelect/LevelOverview/GoToLevelEditorMainMenuStateButtonUI", CanvasLayer.UI)
            .Do<DispatchLevelEditorSetlevelSelectButtonInteractableEventCommand>(false);

        On<LevelEditorLevelSelectButtonClickedEvent>()
            .Do<LevelEditorUpdateLevelNameStatusCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.LevelSelected);

    }

}