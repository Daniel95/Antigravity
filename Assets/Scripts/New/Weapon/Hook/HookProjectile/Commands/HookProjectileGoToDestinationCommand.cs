using IoCPlus;

public class HookProjectileGoToDestinationCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [InjectParameter] private ShootHookEvent.Parameter shootHookEventParameter;

    protected override void Execute() {
        moveTowardsRef.Get().StartMoving(shootHookEventParameter.Destination);
    }
}
