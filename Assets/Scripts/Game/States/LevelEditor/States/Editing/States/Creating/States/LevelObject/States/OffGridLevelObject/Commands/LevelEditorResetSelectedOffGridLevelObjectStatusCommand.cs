using IoCPlus;

public class LevelEditorResetSelectedOffGridLevelObjectStatusCommand : Command {

    protected override void Execute() {
        LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject = null;
    }

}
