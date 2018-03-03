using IoCPlus;

public class UpdateSelectedLevelObjectNodeCommand : Command {

    [InjectParameter] private LevelObjectType levelObjectType;

    protected override void Execute() {
        GenerateableLevelObjectNode generateableLevelObjectNode = GenerateableLevelObjectLibrary.GetNode(levelObjectType);
        SelectedLevelObjectNodeStatus.LevelObjectNode = generateableLevelObjectNode;
    }

}
