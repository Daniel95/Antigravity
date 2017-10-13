using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsNotOnGridCommand : Command {

    protected override void Execute() {
        if(!LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.OnGrid) {
            Abort();
        }
        
    }

}
