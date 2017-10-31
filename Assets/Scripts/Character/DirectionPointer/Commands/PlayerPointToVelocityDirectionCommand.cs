using IoCPlus;
using UnityEngine;

public class PlayerPointToVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        Vector2 velocityDirection = playerVelocityRef.Get().GetVelocityDirection();
        playerDirectionPointerRef.Get().PointToDirection(velocityDirection);
    }

}

