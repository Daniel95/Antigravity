using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand : Command {

    protected override void Execute() {
        LevelObjectTransformType levelObjectTransformType = LevelEditorSelectedLevelObjectNodeStatus.LevelObjectNode.GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelObjectTransformType;
    }

}
