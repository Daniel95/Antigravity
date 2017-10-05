using IoCPlus;

public class LevelEditorRemoveTilesSpawnedByLastSelectionFieldCommand : Command {

    [Inject] private Ref<ILevelEditorTileInput> levelEditorTileInputRef;

    protected override void Execute() {
        levelEditorTileInputRef.Get().RemoveTilesSpawnedByLastSelectionField();
    }

}
