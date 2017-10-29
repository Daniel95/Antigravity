using IoCPlus;
using UnityEngine;

public class AbortIfPlayerSlidingCurrentTargetTransformIsNotColliderTransformCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterSliding> playerSlidingRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        if(playerSlidingRef.Get().CurrentTargetCornerTransform != collider.transform) {
            Abort();
        }
    }

}
