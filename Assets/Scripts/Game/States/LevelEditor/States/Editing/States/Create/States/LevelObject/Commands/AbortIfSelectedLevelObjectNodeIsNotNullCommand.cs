using IoCPlus;

public class AbortIfSelectedLevelObjectNodeIsNotNullCommand : Command {

    protected override void Execute() {
        if(SelectedLevelObjectNodeStatus.LevelObjectNode != null) {
            Abort();
        }
    }

}
