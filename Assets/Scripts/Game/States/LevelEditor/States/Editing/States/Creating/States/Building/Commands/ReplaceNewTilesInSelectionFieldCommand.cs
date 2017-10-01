using IoCPlus;

public class ReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ReplaceNewTilesInSelectionField();
    }

}
