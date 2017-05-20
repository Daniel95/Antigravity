using IoCPlus;

public class DispatchHookProjectileMoveTowardsNextAnchorCommand : Command {

    [Inject] private HookProjectileMoveTowardsNextAnchorEvent hookProjectileMoveTowardsNextAnchorEvent;

    protected override void Execute() {
        hookProjectileMoveTowardsNextAnchorEvent.Dispatch();
    }
}
