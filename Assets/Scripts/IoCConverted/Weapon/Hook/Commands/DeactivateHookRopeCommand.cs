using IoCPlus;

public class DeactivateHookRopeCommand : Command {

    [Inject] private Ref<HookView> hookViewRef;

    protected override void Execute() {
        hookViewRef.Get().DeactivateHookRope();
    }
}
