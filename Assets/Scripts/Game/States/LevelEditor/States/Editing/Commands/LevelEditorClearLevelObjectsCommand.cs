using IoCPlus;

public class LevelEditorClearLevelObjectsCommand : Command {

    [Inject] private LevelEditorLevelObjectsStatus levelObjectsStatus;

    protected override void Execute() {
        levelObjectsStatus.DestroyAllLevelObjects();
    }

}
