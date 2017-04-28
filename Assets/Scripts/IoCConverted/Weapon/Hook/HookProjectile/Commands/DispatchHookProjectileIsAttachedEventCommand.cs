using IoCPlus;

public class DispatchHookProjectileIsAttachedEventCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject] private HookProjectileIsAttachedEvent hookIsAttachedEvent;

    protected override void Execute() {
        hookIsAttachedEvent.Dispatch(hookProjectileRef.Get().HookedLayerIndex);
    }
}
