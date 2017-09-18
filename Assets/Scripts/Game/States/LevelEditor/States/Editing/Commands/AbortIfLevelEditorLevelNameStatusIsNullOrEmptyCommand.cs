using IoCPlus;

public class AbortIfLevelEditorLevelNameStatusLoadedLevelNameIsNullOrEmptyCommand : Command {

    [Inject] private LevelNameStatus LevelNameStatus;

    protected override void Execute() {
        if(string.IsNullOrEmpty(LevelNameStatus.Name)) {
            Abort();
        }
    }

}
