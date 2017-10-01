using IoCPlus;

public class RemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().RemoveTilesInSelectionField();
    }

}
