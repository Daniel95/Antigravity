using IoCPlus;

public class DispatchHookProjectileIsAttachedEventCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject] private HookProjectileIsAttachedEvent hookProjectileIsAttachedEvent;

    protected override void Execute() {
        hookProjectileIsAttachedEvent.Dispatch(hookProjectileRef.Get().HookedLayerIndex);
    }
}
