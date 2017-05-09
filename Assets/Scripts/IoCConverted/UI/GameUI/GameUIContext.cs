using IoCPlus;

public class GameUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GameUIPlayEvent>();
        Bind<GameUIPauseEvent>();

        On<EnterContextSignal>()
            .AddContext<GameUIPauseContext>();

        OnChild<GameUIPauseContext, GameUIPlayEvent>()
            .GotoState<GameUIPlayContext>();

        OnChild<GameUIPlayContext, GameUIPauseEvent>()
            .GotoState<GameUIPauseContext>();
    }

}