using IoCPlus;

public class HookProjectileGoToShootDestinationCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [InjectParameter] private ShootHookEvent.Parameter shootHookEventParameter;

    [Inject] private HookProjectileMoveTowardsShootDestinationCompletedEvent hookProjectileReachedShootDestinationEvent;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(
            shootHookEventParameter.Destination,
            hookProjectileReachedShootDestinationEvent
        );
    }
}
