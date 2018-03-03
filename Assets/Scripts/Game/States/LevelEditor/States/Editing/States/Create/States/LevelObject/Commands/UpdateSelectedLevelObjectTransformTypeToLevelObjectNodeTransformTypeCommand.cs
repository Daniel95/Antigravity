using IoCPlus;

public class UpdateSelectedLevelObjectTransformTypeToLevelObjectNodeTransformTypeCommand : Command {

    protected override void Execute() {
        LevelObjectTransformType levelObjectTransformType = SelectedLevelObjectNodeStatus.LevelObjectNode.GetDefaultLevelObjectInputType();
        SelectedLevelObjectTransformTypeStatus.TransformType = levelObjectTransformType;
    }

}
