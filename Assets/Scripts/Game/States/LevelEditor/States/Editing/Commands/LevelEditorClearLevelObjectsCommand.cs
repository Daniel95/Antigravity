using IoCPlus;

public class LevelEditorClearLevelObjectsCommand : Command {

    [Inject] private Refs<ILevelObject> levelObjectRefs;

    protected override void Execute() {
        for (int i = levelObjectRefs.Count - 1; i >= 0; i--) {
            levelObjectRefs[i].DestroyLevelObject();

        }
    }

}
