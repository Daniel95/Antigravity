using IoCPlus;
using System.Collections.Generic;

public class AbortIfHookStatesAreNotActive : Command<List<HookState>> {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute(List<HookState> hookStates) {
        bool stateIsActive = false;

        hookStates.ForEach(x => {
            if (hookRef.Get().CurrentHookState == x) {
                stateIsActive = true;
            }
        });

        if(!stateIsActive) {
            Abort();
        }
    }
}
