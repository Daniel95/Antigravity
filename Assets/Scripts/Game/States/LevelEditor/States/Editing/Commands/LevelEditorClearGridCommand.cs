using IoCPlus;

public class LevelEditorClearGridCommand : Command {

    protected override void Execute() {
        TileGrid.Clear();
    }

}
