using IoCPlus;

public class LevelEditorContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelEditorStateEvent>();

        On<EnterContextSignal>()
            .GotoState<LevelEditorNavigatingContext>()
            .Do<ShowGridOverlayCommand>(true)
            .Do<SetGridOverlaySizeToScreenWorldSizeCommand>()
            .Do<SetGridOverlayStartToTileSizeCommand>()
            .Do<SetGridOverlayStepToTileSizeCommand>();

        On<LeaveContextSignal>()
            .Do<ShowGridOverlayCommand>(false);

        OnChild<LevelEditorNavigatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Creating)
            .GotoState<LevelEditorCreatingContext>();

        OnChild<LevelEditorCreatingContext, GoToLevelEditorStateEvent>()
            .Do<AbortIfLevelEditorStateIsNotLevelEditorStateCommand>(LevelEditorState.Navigating)
            .GotoState<LevelEditorNavigatingContext>();

    }

}