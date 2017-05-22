using IoCPlus;

public class DispatchHookProjectileMoveTowardsOwnerEventCommand : Command {

    [Inject] private HookProjectileMoveTowardsOwnerEvent hookProjectileMoveTowardsOwnerEvent;

    protected override void Execute() {
        hookProjectileMoveTowardsOwnerEvent.Dispatch();
    }
}
