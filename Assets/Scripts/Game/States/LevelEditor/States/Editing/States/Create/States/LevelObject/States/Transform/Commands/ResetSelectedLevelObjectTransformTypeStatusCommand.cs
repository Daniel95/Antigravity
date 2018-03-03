using IoCPlus;

public class ResetSelectedLevelObjectTransformTypeStatusCommand : Command {

    protected override void Execute() {
        SelectedLevelObjectTransformTypeStatus.TransformType = null;
    }

}
