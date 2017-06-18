using IoCPlus;

public class HookProjectileStopMoveTowardsCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> hookProjectileMoveTowardsRef;

    protected override void Execute() {
        hookProjectileMoveTowardsRef.Get().StopMoving();
    }
}
