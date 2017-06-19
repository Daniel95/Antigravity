using IoCPlus;
using UnityEngine;

public class PlayerPointToCeiledVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;
    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        Vector2 ceiledVelocityDirection = playerVelocityRef.Get().GetCeilVelocityDirection();
        playerDirectionPointerRef.Get().PointToDirection(ceiledVelocityDirection);
    }
}

