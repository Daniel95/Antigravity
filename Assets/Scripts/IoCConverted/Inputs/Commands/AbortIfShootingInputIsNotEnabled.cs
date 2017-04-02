using IoCPlus;

public class AbortIfShootingInputIsNotEnabled : Command {

    [Inject] private InputModel inputModel;

    protected override void Execute() {
        if(!inputModel.shootingInputIsEnabled) {
            Abort();
        }
    }
}