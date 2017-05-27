using IoCPlus;

public class HookProjectileSetParentToCollidingTransformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().SetParent(hookProjectileRef.Get().CollidingTransform);
    }
}
