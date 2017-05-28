using IoCPlus;

public class AbortIfLastHookStateIsHookState : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        if (hookRef.Get().LastHookState == hookState) {
            Abort();
        }
    }
}
