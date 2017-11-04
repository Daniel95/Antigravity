using IoCPlus;
using UnityEngine;

public class PlayerStartCheckingRotateAroundCornerTransformConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterRotateAroundCorner> playerRotateAroundCornerRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        playerRotateAroundCornerRef.Get().StartCheckingRotateAroundCornerTransformConditions(collider.transform, playerVelocityRef.Get().MoveDirection);
    }

}
