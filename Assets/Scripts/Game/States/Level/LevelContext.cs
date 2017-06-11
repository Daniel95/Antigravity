using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStatus>();

        On<EnterContextSignal>()
            .AddContext<LevelUIContext>()
            .AddContext<PlayerContext>()
            .Do<SetCameraTargetCommand>()
            .Do<SetCameraBoundsCommand>();

    }

}