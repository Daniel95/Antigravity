using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<PullingHookPullCommand>()
            .Dispatch<CancelHookEvent>();
    }
}

