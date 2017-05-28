using IoCPlus;
using UnityEngine;

public class DestroyLastHookAnchorCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DestroyAnchorAt(hookRef.Get().Anchors.Count - 1);
    }
}
