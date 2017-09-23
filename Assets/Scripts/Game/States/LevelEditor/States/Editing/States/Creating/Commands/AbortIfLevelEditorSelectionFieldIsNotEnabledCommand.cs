using IoCPlus;

public class AbortIfLevelEditorSelectionFieldIsNotEnabledCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus levelEditorSelectionFieldStatus;

    protected override void Execute() {
        if(!levelEditorSelectionFieldStatus.Enabled) {
            Abort();
        }
    }

}
