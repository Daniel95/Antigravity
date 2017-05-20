using IoCPlus;

public class GrapplingHookStopGrappleLockCommand : Command {

    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        grapplingHookRef.Get().StopGrappleLock();
    }
}
