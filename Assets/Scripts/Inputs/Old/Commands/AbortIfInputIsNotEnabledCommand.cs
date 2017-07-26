using IoCPlus;

public class AbortIfInputIsNotEnabledCommand : Command {

    [Inject] private InputStatus inputStatus;

    protected override void Execute() {
        if(!inputStatus.inputIsEnabled) {
            Abort();
        }
    }
}
