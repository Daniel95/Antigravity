using IoCPlus;

public class HookProjectileDestroyCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().DestroyProjectile();
    }
}
