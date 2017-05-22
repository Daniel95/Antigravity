using IoCPlus;
using UnityEngine;

public class CharacterPointToShootDestinationCommand : Command {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;
    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        Vector2 shootDestinationDirection = (hookRef.Get().Destination - (Vector2)hookRef.Get().Owner.transform.position).normalized;
        directionPointerRef.Get().PointToDirection(shootDestinationDirection);
    }
}

