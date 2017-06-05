using IoCPlus;

public class HookProjectileAbortIfDistanceToOwnerIsHigherThenMinimalDistance : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        float distance = hookProjectileRef.Get().DistanceFromOwner;

        if (distance < hookRef.Get().MinimalDistanceFromOwner) {
            Abort();
        }
    }
}