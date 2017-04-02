using IoCPlus;

public class AbortIfActionInputIsNotEnabled : Command {

    [Inject] private InputModel inputModel;

    protected override void Execute() {
        if(!inputModel.actionInputIsEnabled) {
            Abort();
        }
    }
}