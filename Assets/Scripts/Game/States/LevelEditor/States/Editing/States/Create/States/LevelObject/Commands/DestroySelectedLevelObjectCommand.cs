using IoCPlus;

public class DestroySelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        ILevelObject levelObject = selectedLevelObjectRef.Get();
        levelObject.DestroyLevelObject();
    }

}
