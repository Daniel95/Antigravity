using IoCPlus;

public class UpdateSelectedLevelObjectTransformTypeStatusCommand : Command {

    [InjectParameter] private LevelObjectTransformType levelEditorLevelObjectTransformType;

    protected override void Execute() {
        SelectedLevelObjectTransformTypeStatusView.TransformType = levelEditorLevelObjectTransformType;
    }

}
