using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStatus>();
        Bind<CheckpointStatus>();

        On<EnterContextSignal>()
            .Do<ResetPlayerSessionStatsStatusLevelDeaths>()
            .Do<SetCheckpointToNullCommand>()
            .AddContext<LevelUIContext>()
            .GotoState<PlayerContext>()
            .Do<SetCameraTargetCommand>()
            .Do<SetCameraBoundsCommand>()
            .Do<EnableDragCameraCommand>(false)
            .Do<EnableFollowCameraCommand>(true);

        OnChild<PlayerContext, PlayerRespawnEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<IncreasePlayerSessionStatsStatusLevelDeathsCommand>()
            .GotoState<PlayerContext>()
            .Do<SetCameraTargetCommand>()
            .Do<EnableFollowCameraCommand>(true);
    }

}