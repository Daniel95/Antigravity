using IoCPlus;

public class AbortIfHookDistanceIsLowerThenMinimalDistance : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IGrapplingHook> grapplingHookRef;

    protected override void Execute() {
        float distance = grapplingHookRef.Get().HookDistance;

        if (distance < hookRef.Get().MinimalHookDistance) {
            Abort();
        }
    }
}