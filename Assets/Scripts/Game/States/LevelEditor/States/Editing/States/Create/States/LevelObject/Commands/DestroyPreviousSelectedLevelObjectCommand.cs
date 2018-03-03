using IoCPlus;

public class DestroyPreviousSelectedLevelObjectCommand : Command {

    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    protected override void Execute() {
        ILevelObject levelObject = previousSelectedLevelObjectRef.Get();
        levelObject.DestroyLevelObject();
    }

}
