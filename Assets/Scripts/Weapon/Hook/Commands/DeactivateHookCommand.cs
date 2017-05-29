using IoCPlus;

public class DeactivateHookCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DeactivateHook();
    }
}
