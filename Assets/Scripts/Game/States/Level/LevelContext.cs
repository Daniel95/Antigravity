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
            .Do<SetFollowCameraTargetCommand>()
            .Do<SetCameraBoundsCommand>()
            .Do<EnableFollowCameraCommand>(true);

        On<LeaveContextSignal>()
            .Do<EnableFollowCameraCommand>(false);

        OnChild<PlayerContext, PlayerRespawnEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<IncreasePlayerSessionStatsStatusLevelDeathsCommand>()
            .GotoState<PlayerContext>()
            .Do<SetFollowCameraTargetCommand>()
            .Do<EnableFollowCameraCommand>(true);
    }

}