using IoCPlus;
using UnityEngine;

public class PlayerSetMoveDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        playerVelocityRef.Get().SetMoveDirection(direction);
    }
}
