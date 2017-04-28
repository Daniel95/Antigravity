using IoCPlus;

public class StartHoldHookShotCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().
    }
}
