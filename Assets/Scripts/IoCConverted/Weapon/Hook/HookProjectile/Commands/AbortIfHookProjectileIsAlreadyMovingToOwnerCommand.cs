using IoCPlus;

public class AbortIfHookProjectileIsAlreadyMovingToOwnerCommand : Command {

    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    protected override void Execute() {
        if (hookProjectileRef.Get().IsMovingTowardsOwner) {
            Abort();
        }
    }
}
