using IoCPlus;

public class DispatchEnableActionInputEventCommand : Command<bool> {

    [Inject] private EnableActionInputEvent enableActionInputEvent;

    protected override void Execute(bool enable) {
        enableActionInputEvent.Dispatch(enable);
    }
}
