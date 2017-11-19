using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsNotNullCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        if(selectedLevelObjectRef.Get() != null) {
            Abort();
        }
    }

}
