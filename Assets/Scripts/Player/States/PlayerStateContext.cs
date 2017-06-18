using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CheckpointStatus>();
        Bind<PlayerStateStatus>();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfPlayerCollidingOrInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<CharacterUpdateCollisionDirectionCommand>()
            .Do<DispatchCharacterTurnToNextDirectionEventCommand>()
            .Do<AbortIfSavedCollisionCountIsHigherThenOneCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<SlidingStateContext>();

        On<CollisionExit2DEvent>()
            .Do<AbortIfCollisionDirectionIsNotZeroCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Grappling)
            .GotoState<FloatingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfPlayerNotCollidingAndNotInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<DispatchPlayerBounceEventCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Revived)
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.CheckPoint)
            .Do<SetCheckpointReachedCommand>(true)
            .Do<UpdateCheckpointStatusCommand>()
            .GotoState<RevivedStateContext>();

        On<EnterGrapplingHookContextEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Grappling)
            .GotoState<GrapplingStateContext>();

        On<EnterPullingHookContextSignal>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        On<HoldShotEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<CharacterSetMoveDirectionToVelocityDirectionCommand>()
            .GotoState<FloatingStateContext>();

        On<RespawnPlayerEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Revived)
            .GotoState<RevivedStateContext>();

        OnChild<RevivedStateContext, ReleaseInDirectionInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, JumpInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, CharacterIsStuckEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<SlidingStateContext>();
    }
}