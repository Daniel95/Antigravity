using IoCPlus;

public class InActiveContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<DebugLogMessageCommand>("EnterContextSignal InActiveContext");
    }
}

