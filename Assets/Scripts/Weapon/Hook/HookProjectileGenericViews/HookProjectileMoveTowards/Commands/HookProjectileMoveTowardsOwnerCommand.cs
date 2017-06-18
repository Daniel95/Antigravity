using IoCPlus;

public class HookProjectileMoveTowardsOwnerCommand : Command {

    [Inject] private Ref<IWeapon> weaponRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> hookProjectileMoveTowardsRef;

    [Inject] private HookProjectileMoveTowardsOwnerCompletedEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        hookProjectileMoveTowardsRef.Get().StartMovingToTarget(
            weaponRef.Get().Owner.transform, 
            hookProjectileReturnedToOwnerEvent
        );
    }
}
