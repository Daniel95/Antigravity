using IoCPlus;

public class ActivateHoopRopeCommand : Command {

    [Inject] private Ref<HookView> hookViewRef;

    protected override void Execute() {
        hookViewRef.Get().ActivateHookRope();
    }
}
