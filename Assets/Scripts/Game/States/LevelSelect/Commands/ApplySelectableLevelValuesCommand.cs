using IoCPlus;

public class ApplySelectableLevelValuesCommand : Command {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        selectableLevelFieldRef.Get().ApplySelectableLevelValues();
    }
}
