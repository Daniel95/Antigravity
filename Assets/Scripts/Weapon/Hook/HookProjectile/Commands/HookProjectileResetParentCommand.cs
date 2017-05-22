using IoCPlus;

public class HookProjectileResetParentCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        hookProjectileRef.Get().SetParent(null);
    }
}
