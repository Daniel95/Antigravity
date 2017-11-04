using IoCPlus;
using UnityEngine;

public class PlayerStopAllCheckingRotateAroundCornerConditionsCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterRotateAroundCorner> playerRotateAroundCornerRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerRotateAroundCornerRef.Get().StopAllCheckingRotateAroundCornerConditions();
    }

}
