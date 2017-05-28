using IoCPlus;
using UnityEngine;

public class DestroyOneButLastHookAnchorCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DestroyAnchorAt(hookRef.Get().Anchors.Count - 2);
    }
}
