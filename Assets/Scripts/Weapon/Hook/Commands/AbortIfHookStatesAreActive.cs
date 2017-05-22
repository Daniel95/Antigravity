using IoCPlus;
using System.Collections.Generic;

public class AbortIfHookStatesAreActive : Command<List<HookState>> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(List<HookState> hookStates) {
        if(hookStates.Contains(hookRef.Get().ActiveHookState)) {
            Abort();
        }
    }
}
