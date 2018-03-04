using IoCPlus;

public class ResetSelectedLevelObjectTransformTypeStatusCommand : Command {

    protected override void Execute() {
        SelectedLevelObjectTransformTypeStatusView.TransformType = null;
    }

}
