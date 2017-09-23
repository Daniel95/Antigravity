using IoCPlus;

public class RemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    protected override void Execute() {
        levelEditorTilesRef.Get().RemoveTilesInSelectionField();
    }

}
