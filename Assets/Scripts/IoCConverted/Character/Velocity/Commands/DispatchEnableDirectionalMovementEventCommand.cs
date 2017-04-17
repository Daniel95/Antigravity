using IoCPlus;

public class DispatchEnableDirectionalMovementEventCommand : Command<bool> {

    [Inject] private CharacterEnableDirectionalMovementEvent characterEnableDirecionalMovementEvent;

    protected override void Execute(bool enable) {
        characterEnableDirecionalMovementEvent.Dispatch(enable);
    }
}
