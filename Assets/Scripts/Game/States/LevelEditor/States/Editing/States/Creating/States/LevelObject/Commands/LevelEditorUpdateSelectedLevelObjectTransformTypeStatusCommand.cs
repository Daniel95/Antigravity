using IoCPlus;

public class LevelEditorUpdateSelectedLevelObjectTransformTypeStatusCommand : Command {

    [InjectParameter] private LevelObjectTransformType levelEditorLevelObjectInputType;

    protected override void Execute() {
        LevelEditorSelectedLevelObjectTransformTypeStatus.LevelObjectTransformType = levelEditorLevelObjectInputType;
    }

}
