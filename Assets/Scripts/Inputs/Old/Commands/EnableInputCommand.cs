using IoCPlus;

public class EnableInputCommand : Command<bool> {

    [Inject] private InputStatus inputStatus;

    protected override void Execute(bool enable) {
        inputStatus.inputIsEnabled = enable;
    }
}
