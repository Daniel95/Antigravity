using IoCPlus;

public class GenerateSelectableLevelFieldsCommand : Command {

    [Inject] private Ref<ISelectableLevelField> selectableLevelFieldRef;

    protected override void Execute() {
        selectableLevelFieldRef.Get().GenerateSelectableLevelFields();
    }
}
