using IoCPlus;
using UnityEngine;

public class PlayerRemoveCollisionDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [InjectParameter] private Vector2 collisionDirection;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().RemoveCollisionDirection(collisionDirection);
    }
}
