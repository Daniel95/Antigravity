using IoCPlus;

public class GameUIContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<GameUIPlayPressedEvent>();
        Bind<GameUIPausePressedEvent>();

        On<EnterContextSignal>()
            .GotoState<GameUIPauseContext>();

        OnChild<GameUIPauseContext, GameUIPlayPressedEvent>()
            .GotoState<GameUIPlayContext>();

        OnChild<GameUIPlayContext, GameUIPausePressedEvent>()
            .GotoState<GameUIPauseContext>();
    }

}