using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsOnGridCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.OnGrid) {
            Abort();
        }
        
    }

}
