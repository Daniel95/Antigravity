using IoCPlus;

public class LevelEditorResetSelectedLevelObjectStatusCommand : Command {

    protected override void Execute() {
        LevelEditorSelectedLevelObjectStatus.LevelObject = null;
    }

}
