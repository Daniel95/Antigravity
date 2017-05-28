﻿using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CheckpointStatus>();
        Bind<PlayerStateStatus>();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfCollidingOrInTriggerTagCommand>(Tags.Bouncy)
            .Do<DispatchCharacterTurnToNextDirectionEventCommand>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<SlidingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfNotCollidingAndNotInTriggerTagCommand>(Tags.Bouncy)
            .Do<DispatchCharacterBounceEventCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Revived)
            .Do<AbortIfCollider2DIsNotCheckpointCommand>()
            .Do<UpdateCheckpointStatusCommand>()
            .GotoState<RevivedStateContext>();

        On<EnterGrapplingHookContextEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Grappling)
            .GotoState<GrapplingStateContext>();

        On<RespawnPlayerEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Revived)
            .GotoState<RevivedStateContext>();

        OnChild<RevivedStateContext, ReleaseInDirectionInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, JumpInputEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Floating)
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, NotMovingEvent>()
            .Do<AbortIfPlayerStateStatusStateIsStateCommand>(PlayerStateStatus.PlayerState.Sliding)
            .GotoState<SlidingStateContext>();
    }
}