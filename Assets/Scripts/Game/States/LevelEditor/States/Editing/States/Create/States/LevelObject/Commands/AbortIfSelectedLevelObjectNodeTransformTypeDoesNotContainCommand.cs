using IoCPlus;

public class AbortIfSelectedLevelObjectNodeTransformTypeDoesNotContainCommand : Command<LevelObjectTransformType> {

    protected override void Execute(LevelObjectTransformType levelEditorLevelObjectTransformType) {
        if(!SelectedLevelObjectNodeStatus.LevelObjectNode.TransformTypes.Contains(levelEditorLevelObjectTransformType)) {
            Abort();
        }
    }

}
