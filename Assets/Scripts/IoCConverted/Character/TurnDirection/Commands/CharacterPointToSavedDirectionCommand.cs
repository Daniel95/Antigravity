using IoCPlus;
using UnityEngine;

public class CharacterPointToSavedDirectionCommand : Command {

    [Inject] private Ref<ICharacterTurnDirection> characterTurnDirectionRef;
    [Inject] private Ref<ICharacterDirectionPointer> directionPointerRef;

    protected override void Execute() {
        Vector2 savedDirection = characterTurnDirectionRef.Get().SavedDirection;
        directionPointerRef.Get().PointToDirection(savedDirection);
    }
}
