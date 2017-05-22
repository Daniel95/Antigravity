using IoCPlus;
using UnityEngine;

public class CharacterPointToCeiledVelocityDirectionCommand : Command {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;
    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        Vector2 ceiledVelocityDirection = characterVelocityRef.Get().GetCeilVelocityDirection();
        directionPointerRef.Get().PointToDirection(ceiledVelocityDirection);
    }
}

