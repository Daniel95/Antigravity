using IoCPlus;

public class LevelEditorClearSelectionFieldAvailableGridPositionsCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ClearSelectionFieldAvailableGridPositions();
    }

}
