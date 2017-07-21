using IoCPlus;

public class PullingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<EnterContextSignal>()
            .Do<HookProjectileSetDistanceToOwnerCommand>()
            .Do<AbortIfHookProjectileDistanceToOwnerIsHigherThenMinimalDistance>()
            .Do<PlayerSetMoveDirectionToHookProjectileDirectionCommand>();

        On<EnterContextSignal>()
            .Dispatch<EnterPullingHookContextSignal>()
            .Dispatch<CancelHookEvent>();
    }
}

