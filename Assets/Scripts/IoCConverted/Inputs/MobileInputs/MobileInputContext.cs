using IoCPlus;

public class MobileInputContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<Ref<IMobileInput>>();

        On<EnterContextSignal>()
            .Do<EnableMobileInputCommand>(true);

        On<LeaveContextSignal>()
            .Do<EnableMobileInputCommand>(false);

    }

}