using IoCPlus;
using UnityEngine;

public class HookProjectileSetCollidingTransformToCollider2DTranformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        hookProjectileRef.Get().CollidingTransform = collider.transform;
    }
}
