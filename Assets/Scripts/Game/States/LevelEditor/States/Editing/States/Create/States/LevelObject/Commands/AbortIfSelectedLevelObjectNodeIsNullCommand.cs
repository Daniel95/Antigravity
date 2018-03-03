using IoCPlus;

public class AbortIfSelectedLevelObjectNodeIsNullCommand : Command {

    protected override void Execute() {
        if(SelectedLevelObjectNodeStatus.LevelObjectNode == null) {
            Abort();
        }
    }

}
