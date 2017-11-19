using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectNodeTransformTypeDoesNotContainCommand : Command<LevelObjectTransformType> {

    protected override void Execute(LevelObjectTransformType levelEditorLevelObjectTransformType) {
        if(!LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.TransformTypes.Contains(levelEditorLevelObjectTransformType)) {
            Abort();
        }
    }

}
