using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectStatus.LevelObject == null) {
            Abort();
        }
    }

}
