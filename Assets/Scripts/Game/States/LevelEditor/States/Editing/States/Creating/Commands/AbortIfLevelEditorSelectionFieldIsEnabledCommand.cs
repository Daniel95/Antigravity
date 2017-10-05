using IoCPlus;

public class AbortIfLevelEditorSelectionFieldIsEnabledCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectionFieldStatusView.Enabled) {
            Abort();
        }
    }

}
