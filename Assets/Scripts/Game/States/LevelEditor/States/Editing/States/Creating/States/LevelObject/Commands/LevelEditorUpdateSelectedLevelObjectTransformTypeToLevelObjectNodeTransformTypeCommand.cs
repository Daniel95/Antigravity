using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand : Command {

    protected override void Execute() {
        LevelObjectTransformType levelObjectTransformType = LevelEditorSelectedLevelObjectNodeViewStatus.LevelObjectNode.GetDefaultLevelObjectInputType();
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelObjectTransformType;
    }

}
