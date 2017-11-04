using IoCPlus;
using UnityEngine;

public class PlayerStopCheckingRotateAroundCornerTransformConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterRotateAroundCorner> playerRotateAroundCorner;

    [InjectParameter] private Collider2D collider;

    protected override void Execute() {
        playerRotateAroundCorner.Get().StopCheckingRotateAroundCornerTransformConditions(collider.transform);
    }

}
