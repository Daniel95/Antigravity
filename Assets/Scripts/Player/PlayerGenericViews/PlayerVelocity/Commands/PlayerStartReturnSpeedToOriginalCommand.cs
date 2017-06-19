using IoCPlus;
using UnityEngine;

public class PlayerStartReturnSpeedToOriginalCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [Inject] private float returnSpeed;

    protected override void Execute() {
        playerVelocityRef.Get().StartReturnSpeedToOriginal(returnSpeed);
    }
}
