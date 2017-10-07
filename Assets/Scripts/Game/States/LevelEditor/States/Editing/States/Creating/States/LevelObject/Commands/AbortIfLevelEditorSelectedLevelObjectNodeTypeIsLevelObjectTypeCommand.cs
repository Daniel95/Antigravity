using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeTypeIsLevelObjectTypeCommand : Command<LevelObjectType> {

    protected override void Execute(LevelObjectType levelObjectType) {
        if(LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.LevelObjectType == levelObjectType) {
            Abort();
        }
    }

}
