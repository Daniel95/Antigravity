using IoCPlus;

public class SetSelectionFieldEnabledCommand : Command<bool> {

    protected override void Execute(bool enabled) {
        SelectionFieldStatusView.Enabled = enabled;
    }

}
