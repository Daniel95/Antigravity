using IoCPlus;
using UnityEngine;

public class PlayerResetCollisionDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterCollisionDirection> playerCollisionDirectionRef;

    protected override void Execute() {
        playerCollisionDirectionRef.Get().ResetCollisionDirection();
    }
}
