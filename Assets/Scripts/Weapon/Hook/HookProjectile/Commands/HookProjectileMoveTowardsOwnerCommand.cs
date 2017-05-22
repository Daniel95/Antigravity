using IoCPlus;

public class HookProjectileMoveTowardsOwnerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private HookProjectileMoveTowardsOwnerCompletedEvent hookProjectileReturnedToOwnerEvent;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(
            hookRef.Get().Owner.transform.position, 
            hookProjectileReturnedToOwnerEvent
        );
    }
}
