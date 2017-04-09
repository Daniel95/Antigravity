using IoCPlus;

public class GameContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<PlayerContext>()
            .AddContext<LevelContext>();
    }

}