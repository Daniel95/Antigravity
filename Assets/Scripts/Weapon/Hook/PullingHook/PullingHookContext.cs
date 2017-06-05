using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<HookProjectileSetDistanceToOwnerCommand>()
            .Do<HookProjectileAbortIfDistanceToOwnerIsHigherThenMinimalDistance>()
            .Do<SetMoveDirectionToHookProjectileDirectionCommand>();

        On<EnterContextSignal>()
            .Dispatch<EnterPullingHookContextSignal>()
            .Dispatch<CancelHookEvent>();
    }
}

