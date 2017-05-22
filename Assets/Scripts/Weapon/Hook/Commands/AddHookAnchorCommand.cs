using IoCPlus;
using UnityEngine;

public class AddHookAnchorCommand : Command {

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private Vector2 position;
    [InjectParameter] private Transform parent;

    protected override void Execute() {
        hookRef.Get().AddAnchor(position, parent);
    }
}
