using IoCPlus;

public class LevelEditorRemoveTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().RemoveTilesInSelectionField();
    }

}
