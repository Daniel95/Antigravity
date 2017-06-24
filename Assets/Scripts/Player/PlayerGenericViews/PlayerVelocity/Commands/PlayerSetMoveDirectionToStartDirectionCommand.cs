using IoCPlus;
using UnityEngine;

public class PlayerSetMoveDirectionToStartDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SetMoveDirection(playerVelocityRef.Get().StartDirection);
    }

}
