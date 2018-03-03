using IoCPlus;

public class ClearLevelEditorGridCommand : Command {

    protected override void Execute() {
        TileGrid.Instance.Clear();
    }

}
