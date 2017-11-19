using IoCPlus;

public class AbortIfLevelEditorPreviousSelectedLevelObjectIsNullCommand : Command {

    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    protected override void Execute() {
        if(previousSelectedLevelObjectRef.Get() == null) {
            Abort();
        }
    }

}
