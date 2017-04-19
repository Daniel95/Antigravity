using IoCPlus;

public class DispatchEnableShootingInputEventCommand : Command<bool> {

    [Inject] private EnableShootingInputEvent enableShootingInputEvent;

    protected override void Execute(bool enable) {
        enableShootingInputEvent.Dispatch(enable);
    }
}
