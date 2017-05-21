using IoCPlus;
using UnityEngine;

public class SetHookStateCommand : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState newHookState) {
        Debug.Log("new hook state: " + newHookState);
        hookRef.Get().LastHookState = hookRef.Get().ActiveHookState;
        hookRef.Get().ActiveHookState = newHookState;
    }
}
