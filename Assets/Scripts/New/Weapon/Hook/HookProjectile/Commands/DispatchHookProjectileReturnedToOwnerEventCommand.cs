using IoCPlus;

public class DispatchHookProjectileReturnedToOwnerEventCommand : Command {

    [Inject] private HookProjectileReturnedToOwnerEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        hookProjectileReturnedToOwnerEvent.Dispatch();
    }
}
