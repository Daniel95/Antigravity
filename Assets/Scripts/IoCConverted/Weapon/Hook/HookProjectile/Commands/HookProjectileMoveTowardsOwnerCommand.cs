using IoCPlus;

public class HookProjectileMoveTowardsOwnerCommand : Command {

    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject] private StartMoveTowardsEvent startMoveTowardsEvent;

    protected override void Execute() {
        hookProjectileRef.Get().IsMovingTowardsOwner = true;
        startMoveTowardsEvent.Dispatch(hookRef.Get().Owner.transform.position);
    }
}
