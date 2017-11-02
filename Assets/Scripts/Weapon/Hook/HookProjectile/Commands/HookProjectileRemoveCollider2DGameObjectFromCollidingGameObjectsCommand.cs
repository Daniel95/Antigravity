using IoCPlus;
using UnityEngine;

public class HookProjectileRemoveCollider2DGameObjectFromCollidingGameObjectsCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        hookProjectileRef.Get().CollidingGameObjects.Remove(collider.gameObject);
    }

}
