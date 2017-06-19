using IoCPlus;
using UnityEngine;

public class PlayerPointToDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        playerDirectionPointerRef.Get().PointToDirection(direction);
    }
}
