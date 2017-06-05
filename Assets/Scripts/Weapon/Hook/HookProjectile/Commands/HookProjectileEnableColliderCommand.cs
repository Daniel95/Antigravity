using IoCPlus;

public class HookProjectileEnableColliderCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().SetColliderEnabled(true);
    }
}
