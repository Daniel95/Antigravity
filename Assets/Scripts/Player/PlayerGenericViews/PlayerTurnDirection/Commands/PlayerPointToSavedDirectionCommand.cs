using IoCPlus;
using UnityEngine;

public class PlayerPointToSavedDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnDirectionRef;
    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    protected override void Execute() {
        Vector2 savedDirection = playerTurnDirectionRef.Get().SavedDirection;
        playerDirectionPointerRef.Get().PointToDirection(savedDirection);
    }
}
