using IoCPlus;

public class RemoveTilesSpawnedByLastSelectionFieldCommand : Command {

    [Inject] private Ref<ITileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().RemoveTilesSpawnedByLastSelectionField();
    }

}
