using IoCPlus;

public class GrapplingHookEnterGrappleLockCommand : Command {

    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        grapplingHookRef.Get().EnterGrappleLock();
    }
}
