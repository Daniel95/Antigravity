using IoCPlus;

public class DestroyHookAnchorsCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DestroyAnchors();
    }
}
