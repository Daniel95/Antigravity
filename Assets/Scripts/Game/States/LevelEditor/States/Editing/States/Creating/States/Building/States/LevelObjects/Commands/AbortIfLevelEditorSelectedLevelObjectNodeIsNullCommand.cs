using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode == null) {
            Abort();
        }
    }

}
