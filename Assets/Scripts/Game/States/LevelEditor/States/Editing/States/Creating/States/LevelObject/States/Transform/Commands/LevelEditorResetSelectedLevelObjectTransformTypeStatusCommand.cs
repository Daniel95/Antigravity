using IoCPlus;

public class LevelEditorResetSelectedLevelObjectTransformTypeStatusCommand : Command {

    protected override void Execute() {
        LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType = null;
    }

}
