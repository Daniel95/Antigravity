using IoCPlus;

public class LevelEditorDestroyLevelObjectOfSelectedLevelObjectSectionCommand : Command {

    protected override void Execute() {
        LevelEditorSelectedLevelObjectSectionStatus.LevelObjectSection.DestroyLevelObject();
    }

}
