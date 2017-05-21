using IoCPlus;

public class DispatchHookProjectileReturnedToOwnerEventCommand : Command {

    [Inject] private HookProjectileMoveTowardsOwnerCompletedEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        hookProjectileReturnedToOwnerEvent.Dispatch();
    }
}
