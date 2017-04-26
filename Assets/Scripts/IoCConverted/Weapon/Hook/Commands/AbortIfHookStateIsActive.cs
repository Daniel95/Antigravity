using IoCPlus;

public class AbortIfHookStateIsActive : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        if(hookRef.Get().CurrentHookState == hookState) {
            Abort();
        }
    }
}
