using IoCPlus;

public class LevelUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GoToLevelUIStateEvent>();

        On<EnterContextSignal>()
            .GotoState<LevelUIPauseContext>();

        OnChild<LevelUIPauseContext, GoToLevelUIStateEvent>()
            .Do<AbortIfLevelUIStateIsNotLevelUIStateCommand>(LevelUIState.Play)
            .GotoState<LevelUIPlayContext>();

        OnChild<LevelUIPlayContext, GoToLevelUIStateEvent>()
            .Do<AbortIfLevelUIStateIsNotLevelUIStateCommand>(LevelUIState.Pause)
            .GotoState<LevelUIPauseContext>();
    }

}