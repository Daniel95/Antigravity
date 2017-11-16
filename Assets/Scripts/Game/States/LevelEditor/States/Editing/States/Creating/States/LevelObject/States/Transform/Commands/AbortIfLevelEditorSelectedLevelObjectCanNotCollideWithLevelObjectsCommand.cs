using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectCanNotCollideWithLevelObjectsCommand : Command {

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(LevelEditorSelectedLevelObjectStatus.LevelObject.name);
        if (!generateableLevelObjectNode.CanCollideWithLevelObjects) {
            Abort();
        }
    }

}
