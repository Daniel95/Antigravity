using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsNotNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectStatus.LevelObject != null) {
            Abort();
        }
    }

}
