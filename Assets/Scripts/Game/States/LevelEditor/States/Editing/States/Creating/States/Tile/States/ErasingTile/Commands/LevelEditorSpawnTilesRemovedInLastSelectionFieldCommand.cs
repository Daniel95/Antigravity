using IoCPlus;

public class LevelEditorSpawnTilesRemovedInLastSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().SpawnTilesRemovedInLastSelectionField();
    }

}
