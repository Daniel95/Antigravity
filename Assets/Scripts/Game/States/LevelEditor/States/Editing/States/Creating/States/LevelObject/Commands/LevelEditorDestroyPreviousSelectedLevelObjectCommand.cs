using IoCPlus;

public class LevelEditorDestroyPreviousSelectedLevelObjectCommand : Command {

    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    protected override void Execute() {
        ILevelObject levelObject = previousSelectedLevelObjectRef.Get();
        levelObject.DestroyLevelObject();
    }

}
