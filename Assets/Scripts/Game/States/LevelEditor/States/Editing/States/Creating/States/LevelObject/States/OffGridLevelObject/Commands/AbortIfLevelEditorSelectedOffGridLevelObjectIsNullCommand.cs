using IoCPlus;

public class AbortIfLevelEditorSelectedOffGridLevelObjectIsNullCommand : Command {

    [Inject] private LevelEditorSelectedOffGridLevelObjectStatus selectedOffGridLevelObjectStatus;

    protected override void Execute() {
        if(selectedOffGridLevelObjectStatus.OffGridLevelObject == null) {
            Abort();
        }
    }

}
