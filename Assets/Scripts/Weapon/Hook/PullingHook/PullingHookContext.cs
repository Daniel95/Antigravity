using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<SetMoveDirectionToHookProjectileDirectionCommand>()
            .Dispatch<EnterPullingHookContextSignal>()
            .Dispatch<CancelHookEvent>();
    }
}

