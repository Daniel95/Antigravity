using IoCPlus;
using UnityEngine;

public class AbortIfHookStateIsNotActive : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        Debug.Log("current hook state is " + hookRef.Get().ActiveHookState + " compare with " + hookState);
        if(hookRef.Get().ActiveHookState != hookState) {
            Abort();
        }
    }
}
