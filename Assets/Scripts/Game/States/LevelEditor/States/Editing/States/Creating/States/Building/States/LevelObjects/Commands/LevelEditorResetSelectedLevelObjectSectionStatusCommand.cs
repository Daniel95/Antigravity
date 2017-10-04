using IoCPlus;

public class LevelEditorResetSelectedLevelObjectSectionStatusCommand : Command {

    protected override void Execute() {
        LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection = null;
    }

}
