using IoCPlus;
using UnityEngine;

public class PlayerSetDirectionalMovementCommand : Command<bool> {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute(bool enable) {
        if (enable) {
            playerVelocityRef.Get().EnableDirectionalMovement();
        } else {
            playerVelocityRef.Get().DisableDirectionalMovement();
        }
    }
}
