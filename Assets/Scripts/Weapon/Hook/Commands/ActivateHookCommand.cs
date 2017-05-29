using IoCPlus;

public class ActivateHookCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().ActivateHook();
    }
}
