using IoCPlus;

public class HookProjectileSetParentToAttachedTransformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().SetParent(hookProjectileRef.Get().AttachedTransform);
    }
}
