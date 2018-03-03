using IoCPlus;

public class RemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ITileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().RemoveTilesInSelectionField();
    }

}
