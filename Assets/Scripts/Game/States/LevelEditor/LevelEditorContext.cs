using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        Bind<LevelNameStatus>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorMainMenuContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.MainMenu)
            .GotoState<LevelEditorMainMenuContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Editing)
            .GotoState<LevelEditorEditingContext>();

        On<GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.SelectingLevel)
            .GotoState<LevelEditorEditingContext>();

    }

}