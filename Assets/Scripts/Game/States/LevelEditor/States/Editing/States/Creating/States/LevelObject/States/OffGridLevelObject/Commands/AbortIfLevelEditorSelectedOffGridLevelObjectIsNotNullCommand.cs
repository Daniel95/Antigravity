using IoCPlus;

public class AbortIfLevelEditorSelectedOffGridLevelObjectIsNotNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject != null) {
            Abort();
        }
    }

}
