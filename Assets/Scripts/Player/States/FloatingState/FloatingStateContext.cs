using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetPlayerStateStatusCommand>(PlayerStateStatus.PlayerState.Floating)
            .Do<CharacterPointToVelocityDirectionCommand>()
            .Do<CharacterEnableDirectionalMovementCommand>(true);

    }
}