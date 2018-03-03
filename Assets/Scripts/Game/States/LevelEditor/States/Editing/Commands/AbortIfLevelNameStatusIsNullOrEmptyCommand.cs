using IoCPlus;

public class AbortIfLevelNameStatusLoadedLevelNameIsNullOrEmptyCommand : Command {

    [Inject] private LevelNameStatus levelNameStatus;

    protected override void Execute() {
        if(string.IsNullOrEmpty(levelNameStatus.Name)) {
            Abort();
        }
    }

}
