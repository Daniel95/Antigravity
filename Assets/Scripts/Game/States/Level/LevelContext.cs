using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DebugLogMessageCommand>("EnterContextSignal LevelContext")
            .AddContext<LevelUIContext>()
            .AddContext<PlayerContext>()
            .Do<SetCameraTargetCommand>()
            .Do<SetCameraBoundsCommand>();

    }

}