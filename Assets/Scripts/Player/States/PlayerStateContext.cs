﻿using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<CheckpointStatus>();

        Bind<Ref<IGrapplingState>>();
        Bind<Ref<IRevivedState>>();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfCollidingOrInTriggerTagCommand>(Tags.Bouncy)
            .Do<DispatchCharacterTurnToNextDirectionEventCommand>()
            .GotoState<SlidingStateContext>()
            .OnAbort<DispatchCharacterBounceEventCommand>();

        On<PlayerTriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<AbortIfCollider2DIsNotCheckpointCommand>()
            .Do<UpdateCheckpointStatusCommand>()
            .GotoState<GrapplingStateContext>();

        On<EnterGrapplingHookContextEvent>()
            .GotoState<GrapplingStateContext>();

        On<RespawnPlayerEvent>()
            .GotoState<RevivedStateContext>();

        OnChild<RevivedStateContext, ReleaseInDirectionInputEvent>()
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, StopGrapplingInAirEvent>()
            .GotoState<FloatingStateContext>();

        OnChild<GrapplingStateContext, NotMovingEvent>()
            .GotoState<SlidingStateContext>();
    }
}