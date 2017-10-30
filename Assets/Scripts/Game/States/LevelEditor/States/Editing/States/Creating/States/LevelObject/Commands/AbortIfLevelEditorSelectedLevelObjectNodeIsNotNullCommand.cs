using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeIsNotNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode != null) {
            Abort();
        }
    }

}
