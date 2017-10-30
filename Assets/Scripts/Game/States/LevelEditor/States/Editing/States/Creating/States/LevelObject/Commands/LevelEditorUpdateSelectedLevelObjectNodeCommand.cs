using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectNodeCommand : Command {

    [InjectParameter] private LevelObjectType levelObjectType;

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
        LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode = generateableLevelObjectNode;
    }

}
