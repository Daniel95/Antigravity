using IoCPlus;
using UnityEngine;

public class HookProjectileSetAttachedTransformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private GameObject gameObject;

    protected override void Execute() {
        hookProjectileRef.Get().AttachedTransform = gameObject.transform;
    }
}
