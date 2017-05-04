using IoCPlus;

public class PlayerStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .GotoState<FloatingStateContext>();

        On<ActivateFloatingStateEvent>()
            .GotoState<FloatingStateContext>();

        On<CollisionEnter2DEvent>()
            .Do<AbortIfNotCollidingAndNotInTriggerTagCommand>(Tags.Bouncy)
            .Do<CharacterBounceCommand>()
            .OnAbort<DispatchTurnFromWallEventCommand>();

        On<TurnFromWallEvent>()
            .Dispatch<CharacterTurnToNextDirectionEvent>()
            .Dispatch<ActivateSlidingStateEvent>();

        On<TriggerEnter2DEvent>()
            .Do<AbortIfGameObjectIsNotPlayerCommand>()
            .Do<AbortIfCollider2DIsNotCheckpointCommand>()
            .Do<UpdateCheckpointStatusCommand>()
            .GotoState<GrapplingStateContext>();

    }
}