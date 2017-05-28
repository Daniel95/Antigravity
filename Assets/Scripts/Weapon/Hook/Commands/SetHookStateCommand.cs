using IoCPlus;
using UnityEngine;

public class SetHookStateCommand : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        hookRef.Get().SetHookState(hookState);
    }
}
