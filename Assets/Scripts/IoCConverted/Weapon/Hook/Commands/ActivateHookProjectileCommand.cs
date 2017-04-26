using IoCPlus;

public class ActivateHookProjectileCommand : Command { 

    [Inject] private Ref<IHook> hookRef;

    [InjectParameter] private FireWeaponParameter fireWeaponParameter;

    protected override void Execute() {
        hookRef.Get().ActivateHookProjectile(fireWeaponParameter.spawnPosition);
    }
}
