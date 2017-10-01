using IoCPlus;

public class LevelEditorClearGridCommand : Command {

    protected override void Execute() {
        LevelEditorTileGrid.Instance.Clear();
        LevelEditorLevelObjectSectionGrid.Instance.Clear();
    }

}
