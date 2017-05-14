using IoCPlus;

public class HookProjectileResetAttachedTransformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().AttachedTransform = null;
    }
}
