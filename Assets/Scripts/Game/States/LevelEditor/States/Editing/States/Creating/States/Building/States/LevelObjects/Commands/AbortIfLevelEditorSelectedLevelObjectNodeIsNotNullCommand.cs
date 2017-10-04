using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeIsNotNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode != null) {
            Abort();
        }
    }

}
