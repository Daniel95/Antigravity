using IoCPlus;

public class UnlockNeighboursOfFinishedSelectableLevelsCommand : Command {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        selectableLevelFieldRef.Get().UnlockAllNeighboursOfFinishedSelectableLevels();
    }
}
