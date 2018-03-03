using IoCPlus;

public class ReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ITileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ReplaceNewTilesInSelectionField();
    }

}
