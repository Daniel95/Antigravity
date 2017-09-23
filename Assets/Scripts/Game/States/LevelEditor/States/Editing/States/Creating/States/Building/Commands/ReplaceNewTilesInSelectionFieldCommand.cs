using IoCPlus;

public class ReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTiles> levelEditorTilesRef;

    protected override void Execute() {
        levelEditorTilesRef.Get().ReplaceNewTilesInSelectionField();
    }

}
