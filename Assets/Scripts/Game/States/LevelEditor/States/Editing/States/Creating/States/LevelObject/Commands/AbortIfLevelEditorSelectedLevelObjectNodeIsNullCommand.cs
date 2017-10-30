using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode == null) {
            Abort();
        }
    }

}
