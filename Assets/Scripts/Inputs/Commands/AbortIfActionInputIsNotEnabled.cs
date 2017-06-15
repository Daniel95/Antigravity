using IoCPlus;

public class AbortIfActionInputIsNotEnabled : Command {

    [Inject] private InputStatus inputStatus;

    protected override void Execute() {
        if(!inputStatus.actionInputIsEnabled) {
            Abort();
        }
    }
}