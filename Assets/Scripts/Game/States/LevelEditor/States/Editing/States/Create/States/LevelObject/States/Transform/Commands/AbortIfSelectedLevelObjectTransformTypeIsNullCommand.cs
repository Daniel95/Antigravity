using IoCPlus;

public class AbortIfSelectedLevelObjectTransformTypeIsNullCommand : Command {

    protected override void Execute() {
        if(SelectedLevelObjectTransformTypeStatusView.TransformType == null) {
            Abort();
        }
    }

}
