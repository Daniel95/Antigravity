using IoCPlus;

public class AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedOffGridLevelObjectStatus.OffGridLevelObject == null) {
            Abort();
        }
    }

}
