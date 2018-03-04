using IoCPlus;

public class AbortIfSelectedLevelObjectTransformTypeIsNotCommand : Command<LevelObjectTransformType> {

    protected override void Execute(LevelObjectTransformType levelObjectTransformType) {
        if(SelectedLevelObjectTransformTypeStatusView.TransformType != levelObjectTransformType) {
            Abort();
        }
    }

}
