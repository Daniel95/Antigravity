using IoCPlus;

public class AbortIfLevelEditorPreviousSelectedLevelObjectIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectStatus.PreviousLevelObject == null) {
            Abort();
        }
    }

}
