using IoCPlus;

public class LevelEditorRemoveTilesInSelectionFieldOnAvailableGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
