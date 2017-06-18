using IoCPlus;

public class PlayerMoveTowardsView : MoveTowardsView {

    [Inject(Label.Player)] private Ref<IMoveTowards> playerMoveTowardsRef;

    public override void Initialize() {
        playerMoveTowardsRef.Set(this);
    }
}
