using IoCPlus;

public class EnableMobileInputCommand : Command<bool> {

    [Inject] private Ref<IMobileInput> mobileInputRef;

    protected override void Execute(bool enable) {
        mobileInputRef.Get().EnableInput(enable);
    }
}
