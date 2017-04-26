using IoCPlus;

public class DeactivateHoopRopeCommand : Command {

    [Inject] private Ref<HookView> hookViewRef;

    protected override void Execute() {
        hookViewRef.Get().DeactivateHookRope();
    }
}
