using IoCPlus;

public class HookProjectileMoveToShootDirectionCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    [InjectParameter] private ShootHookEvent.Parameter shootHookEventParameter;

    protected override void Execute() {
        moveTowardsRef.Get().StartMovingToDirection(shootHookEventParameter.Direction);
    }
}
