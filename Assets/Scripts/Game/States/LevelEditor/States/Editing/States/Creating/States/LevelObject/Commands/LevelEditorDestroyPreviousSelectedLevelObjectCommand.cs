using IoCPlus;

public class LevelEditorDestroyPreviousSelectedLevelObjectCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelEditorLevelObjectsStatus;

    protected override void Execute() {
        levelEditorLevelObjectsStatus.DestroyLevelObject(LevelEditorSelectedLevelObjectStatus.PreviousLevelObject);
    }

}
