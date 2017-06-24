using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStateStatus>();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerCollidingOrInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<PlayerUpdateCollisionDirectionCommand>()
            .Do<DispatchPlayerTurnToNextDirectionEventCommand>()
            .Do<AbortIfSavedCollisionCountIsHigherThenOneCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtStart)
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtCheckpoint)
            .GotoState<SlidingStateContext>();

        On<PlayerCollisionExit2DEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtStart)
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtCheckpoint)
            .GotoState<FloatingStateContext>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtCheckpoint)
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.CheckPoint)
            .Do<UpdateCheckpointStatusCommand>()
            .GotoState<RevivedAtCheckpointStateContext>();

        On<EnterGrapplingHookContextEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Grappling)
            .GotoState<GrapplingStateContext>();

        On<EnterPullingHookContextSignal>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        On<HoldShotEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<PlayerSetMoveDirectionToVelocityDirectionCommand>()
            .GotoState<FloatingStateContext>();

        On<PlayerRespawnAtCheckpointEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtCheckpoint)
            .Do<SetPlayerPositionToCheckpointPositionCommand>()
            .GotoState<RevivedAtCheckpointStateContext>();

        On<PlayerRespawnAtStartEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RevivedAtStart)
            .GotoState<RevivedAtStartContext>();

        OnChild<RevivedAtCheckpointStateContext, ReleaseInDirectionInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, JumpInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, PlayerIsStuckEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<SlidingStateContext>();
    }
}