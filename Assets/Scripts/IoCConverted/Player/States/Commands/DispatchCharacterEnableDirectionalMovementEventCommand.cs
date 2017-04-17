using IoCPlus;

public class DispatchCharacterEnableDirectionalMovementEventCommand : Command<bool> {

    [Inject] private CharacterEnableDirectionalMovementEvent characterEnableDirectionalMovementEvent;

    protected override void Execute(bool enable) {
        characterEnableDirectionalMovementEvent.Dispatch(enable);
    }
}
