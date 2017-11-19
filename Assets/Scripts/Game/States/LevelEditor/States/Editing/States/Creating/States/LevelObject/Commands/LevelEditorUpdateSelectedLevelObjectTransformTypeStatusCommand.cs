using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeStatusCommand : Command {

    [InjectParameter] private LevelObjectTransformType levelEditorLevelObjectTransformType;

    protected override void Execute() {
        LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType = levelEditorLevelObjectTransformType;
    }

}
