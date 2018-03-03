using IoCPlus;

public class SpawnTilesRemovedInLastSelectionFieldCommand : Command {

    [Inject] private Ref<ITileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().SpawnTilesRemovedInLastSelectionField();
    }

}
