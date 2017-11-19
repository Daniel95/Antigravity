using IoCPlus;

public class LevelEditorDestroySelectedLevelObjectCommand : Command {

    [Inject(Label.SelectedLevelObject)] private Ref<ILevelObject> selectedLevelObjectRef;

    protected override void Execute() {
        ILevelObject levelObject = selectedLevelObjectRef.Get();
        levelObject.DestroyLevelObject();
    }

}
