using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<ActivateSlidingStateEvent>();

        On<EnterContextSignal>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<DispatchCharacterEnableDirectionalMovementEventCommand>(true);

        On<CollisionEnter2DEvent>()
            .Do<AbortIfCollisionTagIsNotTheSame>(Tags.Bouncy)
            .Do<CharacterBounceCommand>()
            .OnAbort<DispatchTurnFromWallEventCommand>();

        On<TurnFromWallEvent>()
            .Dispatch<CharacterTurnToNextDirectionEvent>()
            .Dispatch<ActivateSlidingStateEvent>();
    }
}