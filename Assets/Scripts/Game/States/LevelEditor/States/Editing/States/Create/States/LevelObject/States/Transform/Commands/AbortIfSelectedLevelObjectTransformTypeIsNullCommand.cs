using IoCPlus;

public class AbortIfSelectedLevelObjectTransformTypeIsNullCommand : Command {

    protected override void Execute() {
        if(SelectedLevelObjectTransformTypeStatus.TransformType == null) {
            Abort();
        }
    }

}
