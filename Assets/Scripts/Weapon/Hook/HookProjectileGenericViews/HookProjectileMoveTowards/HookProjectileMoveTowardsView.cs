using IoCPlus;

public class HookProjectileMoveTowardsView : MoveTowardsView {

    [Inject(Label.HookProjectile)] private Ref<IMoveTowards> hookProjectileMoveTowardsRef;

    public override void Initialize() {
        hookProjectileMoveTowardsRef.Set(this);
    }
}
