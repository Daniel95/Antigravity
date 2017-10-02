using IoCPlus;

public class LevelEditorLevelOverViewContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DispatchLevelEditorSetlevelSelectButtonInteractableEventCommand>(true);

        On<LeaveContextSignal>()
            .Do<DispatchLevelEditorSetlevelSelectButtonInteractableEventCommand>(false);

        On<LevelEditorLevelSelectButtonClickedEvent>()
            .Do<LevelEditorUpdateLevelNameStatusCommand>()
            .Do<DispatchGoToLevelEditorStateEventCommand>(LevelEditorState.LevelSelected);

    }

}