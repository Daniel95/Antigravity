using IoCPlus;
using System.Collections.Generic;

public class AbortIfHookStatesAreActive : Command<List<HookState>> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(List<HookState> hookStates) {
        hookStates.ForEach(x => {
            if (hookRef.Get().CurrentHookState == x) {
                Abort();
            }
        });
    }
}
