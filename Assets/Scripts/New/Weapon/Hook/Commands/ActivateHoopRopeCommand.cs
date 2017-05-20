using IoCPlus;

public class ActivateHoopRopeCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().ActivateHookRope();
    }
}
