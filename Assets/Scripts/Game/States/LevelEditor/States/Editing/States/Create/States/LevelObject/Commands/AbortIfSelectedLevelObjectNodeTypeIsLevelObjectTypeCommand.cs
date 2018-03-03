using IoCPlus;

public class AbortIfSelectedLevelObjectNodeTypeIsLevelObjectTypeCommand : Command<LevelObjectType> {

    protected override void Execute(LevelObjectType levelObjectType) {
        if(SelectedLevelObjectNodeStatus.LevelObjectNode.LevelObjectType == levelObjectType) {
            Abort();
        }
    }

}
