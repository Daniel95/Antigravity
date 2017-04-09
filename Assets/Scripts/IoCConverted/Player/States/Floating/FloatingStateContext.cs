using IoCPlus;

public class FloatingStateContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        Bind<ActivateSlidingStateEvent>();

        On<EnterContextSignal>()
            .Do<ActivateViewOnPlayerCommand<FloatingStateView>>();

    }

}