using IoCPlus;
using UnityEngine;

public class CharacterPointToDirectionCommand : Command {

    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        directionPointerRef.Get().PointToDirection(direction);
    }
}
