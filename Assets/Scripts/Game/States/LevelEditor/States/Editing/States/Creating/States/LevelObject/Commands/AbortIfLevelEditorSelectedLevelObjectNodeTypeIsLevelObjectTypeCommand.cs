using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeTypeIsLevelObjectTypeCommand : Command<LevelObjectType> {

    protected override void Execute(LevelObjectType levelObjectType) {
        if(LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.LevelObjectType == levelObjectType) {
            Abort();
        }
    }

}
