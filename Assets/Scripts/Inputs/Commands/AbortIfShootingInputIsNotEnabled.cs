using IoCPlus;

public class AbortIfShootingInputIsNotEnabled : Command {

    [Inject] private InputStatus inputStatus;

    protected override void Execute() {
        if(!inputStatus.shootingInputIsEnabled) {
            Abort();
        }
    }
}