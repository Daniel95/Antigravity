using IoCPlus;
using UnityEngine;

public class PlayerCollisionHitDetectionView : CollisionHitDetectionView {

    [Inject] private PlayerRemoveSavedCollisionEvent playerRemoveSavedColliderEvent;

    [Inject(Label.Player)] private Ref<ICollisionHitDetection> playerCollisionHitDetectionRef;

    public override void Initialize() {
        playerCollisionHitDetectionRef.Set(this);
    }

    protected override void OnContactPointChange(Collision2D collision) {
        playerRemoveSavedColliderEvent.Dispatch(collision);
        base.OnContactPointChange(collision);
    }

}
