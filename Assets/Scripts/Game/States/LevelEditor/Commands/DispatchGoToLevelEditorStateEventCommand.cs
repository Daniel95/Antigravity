using IoCPlus;

public class DispatchGoToLevelEditorStateEventCommand : Command<LevelEditorState> {

    [Inject] private GoToLevelEditorStateEvent goToLevelEditorStateEvent;

    protected override void Execute(LevelEditorState levelEditorState) {
        goToLevelEditorStateEvent.Dispatch(levelEditorState);
    }

}
