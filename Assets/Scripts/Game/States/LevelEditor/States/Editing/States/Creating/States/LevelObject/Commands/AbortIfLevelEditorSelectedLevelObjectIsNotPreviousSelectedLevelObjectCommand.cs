using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectIsPreviousSelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;
    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    protected override void Execute() {
        if(selectedLevelObjectRef.Get() == previousSelectedLevelObjectRef.Get()) {
            Abort();
        }
    }

}
