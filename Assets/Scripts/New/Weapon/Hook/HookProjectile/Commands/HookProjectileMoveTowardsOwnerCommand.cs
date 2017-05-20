using IoCPlus;

public class HookProjectileMoveTowardsOwnerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private HookProjectileReturnedToOwnerEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        hookProjectileRef.Get().IsMovingTowardsOwner = true;
        moveTowardsRef.Get().StartMoving(
            hookRef.Get().Owner.transform.position, 
            hookProjectileReturnedToOwnerEvent
        );
    }
}
