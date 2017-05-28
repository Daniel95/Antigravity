using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DebugLogMessageCommand>("LeaveContextSignal PullingHookContext")
            .Do<PullingHookPullCommand>()
            .Dispatch<CancelHookEvent>();

    }
}

