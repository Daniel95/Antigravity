using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<CharacterEnableDirectionalMovementCommand>(true);

    }
}