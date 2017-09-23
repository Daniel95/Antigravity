using IoCPlus;

public class AbortIfLevelEditorSelectionFieldIsEnabledCommand : Command {

    [Inject] private LevelEditorSelectionFieldStatus levelEditorSelectionFieldStatus;

    protected override void Execute() {
        if(levelEditorSelectionFieldStatus.Enabled) {
            Abort();
        }
    }

}
