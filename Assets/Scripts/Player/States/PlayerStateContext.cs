using IoCPlus;
using System.Collections.Generic;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<PlayerStateStatus>();

        On<EnterContextSignal>()
            .GotoState<PlayerFloatingContext>();

        On<PlayerCollisionEnter2DEvent>()
            .Do<AbortIfPlayerCollidingOrInTriggerWithTagCommand>(Tags.Bouncy)
            .Do<PlayerUpdateCollisionDirectionCommand>()
            .Do<DispatchPlayerTurnToNextDirectionEventCommand>()
            .Do<AbortIfSavedCollisionCountIsHigherThenOneCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStatesCommand>(new List<PlayerStateStatus.PlayerState> {
                PlayerStateStatus.PlayerState.Sliding,
                PlayerStateStatus.PlayerState.RespawnAtStart,
                PlayerStateStatus.PlayerState.RespawnAtCheckpoint,
            })
            .GotoState<PlayerSlidingContext>();

        On<PlayerCollisionExit2DEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStatesCommand>(new List<PlayerStateStatus.PlayerState> {
                PlayerStateStatus.PlayerState.Floating,
                PlayerStateStatus.PlayerState.Grappling,
                PlayerStateStatus.PlayerState.RespawnAtStart,
                PlayerStateStatus.PlayerState.RespawnAtCheckpoint,
            })
            .GotoState<PlayerFloatingContext>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RespawnAtCheckpoint)
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.CheckPoint)
            .Do<UpdateCheckpointStatusCommand>()
            .Dispatch<CancelDragInputEvent>()
            .GotoState<PlayerStartAtCheckpointContext>();

        On<EnterGrapplingHookContextEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Grappling)
            .GotoState<PlayerGrapplingContext>();

        On<EnterPullingHookContextSignal>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        On<HoldShotEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<PlayerSetMoveDirectionToVelocityDirectionCommand>()
            .GotoState<PlayerFloatingContext>();

        On<PlayerStartAtCheckpointEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RespawnAtCheckpoint)
            .Do<SetPlayerPositionToCheckpointPositionCommand>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerPointToSavedDirectionCommand>()
            .GotoState<PlayerStartAtCheckpointContext>();

        On<PlayerStartAtStartPointEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.RespawnAtStart)
            .GotoState<PlayerStartAtStartPointContext>();

        OnChild<PlayerStartAtStartPointContext, PlayerStartAtStartPointCompletedEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerStartAtCheckpointContext, ReleaseInDirectionInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerGrapplingContext, PlayerTryJumpEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerGrapplingContext, PlayerIsStuckEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<PlayerSlidingContext>();
    }
}