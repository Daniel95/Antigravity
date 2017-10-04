using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection == null) {
            Abort();
        }
    }

}
