using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorCreatingContext>();

        OnChild<LevelEditorNavigatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Creating)
            .GotoState<LevelEditorCreatingContext>();

        OnChild<LevelEditorCreatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Navigating)
            .GotoState<LevelEditorNavigatingContext>();

    }

}