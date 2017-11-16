using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithTilesCommand : Command {

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(LevelEditorSelectedLevelObjectStatus.LevelObject.name);
        if (!generateableLevelObjectNode.CanCollideWithTiles) {
            Abort();
        }
    }

}
