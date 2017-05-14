using IoCPlus;

public class DeactivateHookRopeCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DeactivateHookRope();
    }
}
