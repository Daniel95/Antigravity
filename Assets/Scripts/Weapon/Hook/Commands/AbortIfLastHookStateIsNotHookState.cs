using IoCPlus;
using UnityEngine;

public class AbortIfLastHookStateIsNotHookState : Command<HookState> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(HookState hookState) {
        if (hookRef.Get().LastHookState != hookState) {
            Abort();
        }
    }
}
