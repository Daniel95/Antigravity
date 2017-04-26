using IoCPlus;

public class DeactivateHookProjectileCommand : Command { 

    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        hookRef.Get().DeactivateHookProjectile();
    }
}
