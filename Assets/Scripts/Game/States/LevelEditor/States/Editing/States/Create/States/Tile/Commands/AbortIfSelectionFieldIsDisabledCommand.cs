using IoCPlus;

public class AbortIfSelectionFieldIsDisabledCommand : Command {

    protected override void Execute() {
        if(!SelectionFieldStatusView.Enabled) {
            Abort();
        }
    }

}
