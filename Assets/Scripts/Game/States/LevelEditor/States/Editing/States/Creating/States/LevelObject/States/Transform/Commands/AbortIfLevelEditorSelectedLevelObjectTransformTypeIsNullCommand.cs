using IoCPlus;

public class AbortIfLevelEditorSelectedLevelObjectTransformTypeIsNullCommand : Command {

    protected override void Execute() {
        if(LevelEditorSelectedLevelObjectTransformTypeStatus.TransformType == null) {
            Abort();
        }
    }

}
