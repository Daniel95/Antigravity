using IoCPlus;

public class AbortIfLevelEditorLevelNameStatusLoadedLevelNameIsNullOrEmptyCommand : Command {

    [Inject] private SavedLevelNameStatus LevelNameStatus;

    protected override void Execute() {
        if(string.IsNullOrEmpty(LevelNameStatus.Name)) {
            Abort();
        }
    }

}
