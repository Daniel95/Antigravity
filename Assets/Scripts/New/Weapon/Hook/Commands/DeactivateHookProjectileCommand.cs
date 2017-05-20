using IoCPlus;

public class DeactivateHookProjectileCommand : Command { 

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().DeactivateHookProjectile();
    }
}
