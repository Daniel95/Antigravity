using IoCPlus;

public class HookProjectileMoveToShootDirectionCommand : Command {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> hookProjectileMoveTowardsRef;

    [Inject] private Ref<IWeapon> weaponRef;
    [InjectParameter] private ShootHookEvent.Parameter shootHookEventParameter;

    protected override void Execute() {
        hookProjectileMoveTowardsRef.Get().StartMovingToDirection(shootHookEventParameter.Direction);
    }
}
