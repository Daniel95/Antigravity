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
            .Do<AbortIfPlayerStateStatusStateIsStatesCommand>(new List<PlayerState> {
                PlayerState.Sliding,
                PlayerState.RespawnAtStart,
                PlayerState.RespawnAtCheckpoint,
            })
            .GotoState<PlayerSlidingContext>();

        On<PlayerCollisionExit2DEvent>()
            .Do<AbortIfPlayerCollisionDirectionIsNotZeroCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStatesCommand>(new List<PlayerState> {
                PlayerState.Floating,
                PlayerState.Grappling,
                PlayerState.RespawnAtStart,
                PlayerState.RespawnAtCheckpoint,
            })
            .GotoState<PlayerFloatingContext>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.RespawnAtCheckpoint)
            .Do<AbortIfTriggerTagIsNotTheSameCommand>(Tags.CheckPoint)
            .Do<UpdateCheckpointStatusCommand>()
            .Dispatch<CancelDragInputEvent>()
            .GotoState<PlayerStartAtCheckpointContext>();

        On<EnterGrapplingHookContextEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Grappling)
            .GotoState<PlayerGrapplingContext>();

        On<EnterPullingHookContextSignal>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        On<HoldShotEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Floating)
            .Do<PlayerSetMoveDirectionToVelocityDirectionCommand>()
            .GotoState<PlayerFloatingContext>();

        On<PlayerStartAtCheckpointEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.RespawnAtCheckpoint)
            .Do<SetPlayerPositionToCheckpointPositionCommand>()
            .Do<PlayerSetSavedDirectionToStartDirectionCommand>()
            .Do<PlayerPointToSavedDirectionCommand>()
            .GotoState<PlayerStartAtCheckpointContext>();

        On<PlayerStartAtStartPointEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.RespawnAtStart)
            .GotoState<PlayerStartAtStartPointContext>();

        OnChild<PlayerStartAtStartPointContext, PlayerStartAtStartPointCompletedEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerStartAtCheckpointContext, ReleaseInDirectionInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerGrapplingContext, PlayerTryJumpEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Floating)
            .GotoState<PlayerFloatingContext>();

        OnChild<PlayerGrapplingContext, PlayerIsStuckEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerState.Sliding)
            .GotoState<PlayerSlidingContext>();
    }
}