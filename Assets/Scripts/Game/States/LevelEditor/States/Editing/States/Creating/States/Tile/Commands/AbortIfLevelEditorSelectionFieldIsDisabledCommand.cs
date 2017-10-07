using IoCPlus;

public class AbortIfLevelEditorSelectionFieldIsDisabledCommand : Command {

    protected override void Execute() {
        if(!LevelEditorSelectionFieldStatusView.Enabled) {
            Abort();
        }
    }

}
