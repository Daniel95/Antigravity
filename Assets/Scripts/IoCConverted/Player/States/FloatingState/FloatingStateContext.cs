using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<CharacterPointToCeiledVelocityDirectionCommand>()
            .Do<DispatchCharacterEnableDirectionalMovementEventCommand>(true);

    }
}