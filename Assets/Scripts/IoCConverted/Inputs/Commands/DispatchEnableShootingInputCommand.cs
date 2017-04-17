using IoCPlus;

public class DispatchEnableShootingInputCommand : Command<bool> {

    [Inject] private EnableShootingInputEvent enableShootingInputEvent;

    protected override void Execute(bool enable) {
        enableShootingInputEvent.Dispatch(enable);
    }
}
