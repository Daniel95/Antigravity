using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand : Command<LevelObjectTransformType> {

    protected override void Execute(LevelObjectTransformType levelEditorLevelObjectTransformType) {
        if(!LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.AvailableInputTypes.Contains(levelEditorLevelObjectTransformType)) {
            Abort();
        }
    }

}
