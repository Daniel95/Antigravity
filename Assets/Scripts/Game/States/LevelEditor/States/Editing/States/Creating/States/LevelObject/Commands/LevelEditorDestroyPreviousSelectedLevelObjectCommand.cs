using IoCPlus;

public class LevelEditorDestroyPreviousSelectedLevelObjectCommand : Command {

    [Inject(Label.PreviousSelectedLevelObject)] private Ref<ILevelObject> previousSelectedLevelObjectRef;

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        levelEditorLevelObjectsStatus.DestroyLevelObject(previousSelectedLevelObjectRef.Get().GameObject);
    }

}
