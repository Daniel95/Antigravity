using IoCPlus;

public class LevelEditorReplaceNewTilesInSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().ReplaceNewTilesInSelectionField();
    }

}
