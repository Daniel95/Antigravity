using IoCPlus;

public class HookProjectileResetCollidingTransformCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().CollidingTransform = null;
    }
}
