using IoCPlus;

public class HookProjectileDisableColliderCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().SetColliderEnabled(false);
    }
}
