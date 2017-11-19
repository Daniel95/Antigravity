using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorMainMenuContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.MainMenu)
            .GotoState<LevelEditorMainMenuContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Editing)
            .GotoState<LevelEditorEditingContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.LevelSelect)
            .GotoState<LevelEditorLevelSelectContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Testing)
            .GotoState<LevelEditorTestingContext>();

    }

}