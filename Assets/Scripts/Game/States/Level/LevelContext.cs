using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .AddContext<GameUIContext>()
            .AddContext<PlayerContext>()
            .AddContext<CameraContext>();

    }

}