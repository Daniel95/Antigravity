using IoCPlus;

public class LevelEditorClearSelectionFieldAvailableGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    protected override void Execute() {
        levelEditorTilesRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
