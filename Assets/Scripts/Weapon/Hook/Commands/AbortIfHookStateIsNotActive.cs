using IoCPlus;
using UnityEngine;

public class AbortIfHookStateIsNotActive : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        if(hookRef.Get().ActiveHookState != hookState) {
            Abort();
        }
    }
}
