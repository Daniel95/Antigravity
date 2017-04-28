using IoCPlus;

public class StopHoldHookShotCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().
    }
}
