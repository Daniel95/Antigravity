using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectSectionStatusIsNotNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection != null) {
            Abort();
        }
    }

}
