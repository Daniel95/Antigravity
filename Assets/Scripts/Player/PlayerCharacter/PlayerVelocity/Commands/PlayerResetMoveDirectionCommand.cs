using IoCPlus;
using UnityEngine;

public class PlayerResetMoveDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SetMoveDirection(Vector2.zero);
    }
}
