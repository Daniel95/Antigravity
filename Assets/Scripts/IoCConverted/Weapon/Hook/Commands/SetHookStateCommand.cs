using IoCPlus;

public class SetHookStateCommand : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState newHookState) {
        hookRef.Get().CurrentHookState = newHookState;
    }
}
