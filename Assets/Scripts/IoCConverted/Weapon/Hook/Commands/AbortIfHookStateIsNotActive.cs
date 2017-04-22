using IoCPlus;

public class AbortIfHookStateIsNotActive : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        hookRef.Get().
    }
}
