using IoCPlus;

public class DispatchHookProjectileMoveTowardsNextAnchorCommand : Command {

    [Inject] private HookProjectileMoveTowardsNextAnchorEvent returnToNextAnchorOrOwnerByIndexEvent;

    protected override void Execute() {
        returnToNextAnchorOrOwnerByIndexEvent.Dispatch();
    }
}
