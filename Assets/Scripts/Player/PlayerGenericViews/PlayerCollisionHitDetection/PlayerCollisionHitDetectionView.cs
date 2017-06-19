using IoCPlus;

public class PlayerCollisionHitDetectionView : CollisionHitDetectionView {

    [Inject(Label.Player)] private Ref<ICollisionHitDetection> playerCollisionHitDetectionRef;

    public override void Initialize() {
        playerCollisionHitDetectionRef.Set(this);
    }
}
