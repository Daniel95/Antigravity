using IoCPlus;

public class LevelEditorClearLevelObjectsCommand : Command {

    [Inject] private Refs<ILevelObject> levelObjectRefs;

    protected override void Execute() {
        foreach (ILevelObject levelObject in levelObjectRefs) {
            levelObject.DestroyLevelObject();
        }
    }

}
