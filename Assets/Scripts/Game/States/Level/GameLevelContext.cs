using IoCPlus;

public class GameLevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<LevelUIContext>()
            .GotoState<LevelContext>();

    }
}
