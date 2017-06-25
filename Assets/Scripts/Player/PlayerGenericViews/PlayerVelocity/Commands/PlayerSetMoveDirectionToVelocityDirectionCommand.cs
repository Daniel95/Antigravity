using IoCPlus;
using UnityEngine;

public class PlayerSetMoveDirectionToVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        Debug.Log(playerVelocityRef.Get().GetVelocityDirection());
        playerVelocityRef.Get().SetMoveDirection(playerVelocityRef.Get().GetVelocityDirection());
    }
}
