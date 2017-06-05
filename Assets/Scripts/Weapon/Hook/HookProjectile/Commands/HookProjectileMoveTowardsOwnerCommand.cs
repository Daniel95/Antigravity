using IoCPlus;

public class HookProjectileMoveTowardsOwnerCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private HookProjectileMoveTowardsOwnerCompletedEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        moveTowardsRef.Get().StartMovingToTarget(
            weaponRef.Get().Owner.transform, 
            hookProjectileReturnedToOwnerEvent
        );
    }
}
