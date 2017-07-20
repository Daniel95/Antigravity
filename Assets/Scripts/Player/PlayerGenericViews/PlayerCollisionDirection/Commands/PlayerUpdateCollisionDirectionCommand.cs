using IoCPlus;
using UnityEngine;

public class PlayerUpdateCollisionDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    [InjectParameter] private Collision2D collision;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().UpdateCollisionDirection(collision);
    }
}
