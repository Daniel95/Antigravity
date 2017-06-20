using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStatus>();
        Bind<CheckpointStatus>();

        On<EnterContextSignal>()
            .Do<SetCheckpointToNullCommand>()
            .AddContext<LevelUIContext>()
            .AddContext<PlayerContext>()
            .Do<SetCameraTargetCommand>()
            .Do<SetCameraBoundsCommand>()
            .Do<EnableDragCameraCommand>(false)
            .Do<EnableFollowCameraCommand>(true);

    }

}