using IoCPlus;

public class HookProjectileStopMoveTowardsCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> moveTowardsRef;

    protected override void Execute() {
        moveTowardsRef.Get().StopMoving();
    }
}
