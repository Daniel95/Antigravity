using IoCPlus;

public class LevelContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStatus>();
        Bind<CheckpointStatus>();

        On<EnterContextSignal>()
            .Do<ResetPlayerSessionStatsStatusLevelDeaths>()
            .Do<SetCheckpointToNullCommand>()
            .GotoState<PlayerContext>()
            .Do<SetFollowCameraTargetCommand>()
            .Do<EnableFollowCameraCommand>(true);

        On<EnterContextSignal>()
            .Do<AbortIfCameraBoundsAreNullCommand>()
            .Do<SetCameraBoundsCommand>();

        On<LeaveContextSignal>()
            .Do<DestroyPlayerCommand>()
            .Do<EnableFollowCameraCommand>(false);

        OnChild<PlayerContext, PlayerRespawnEvent>()
            .Do<WaitFramesCommand>(1)
            .Do<IncreasePlayerSessionStatsStatusLevelDeathsCommand>()
            .GotoState<PlayerContext>()
            .Do<SetFollowCameraTargetCommand>()
            .Do<EnableFollowCameraCommand>(true);
    }

}