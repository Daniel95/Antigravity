using IoCPlus;

public class AbortIfLevelEditorSelectionFieldIsEnabledCommand : Command {

    protected override void Execute() {
        if(SelectionFieldStatusView.Enabled) {
            Abort();
        }
    }

}
