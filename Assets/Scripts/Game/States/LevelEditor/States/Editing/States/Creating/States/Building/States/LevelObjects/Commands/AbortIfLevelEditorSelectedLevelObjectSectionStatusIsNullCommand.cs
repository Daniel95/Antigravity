using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand : Command {

    [Inject] private LevelEditorSelectedLevelObjectSectionStatus levelEditorSelectedLevelObjectSectionStatus;

    protected override void Execute() {
        if(levelEditorSelectedLevelObjectSectionStatus.LevelObjectSection == null) {
            Abort();
        }
    }

}
