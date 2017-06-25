using IoCPlus;
using UnityEngine;

public class PlayerSwitchDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SwitchMoveDirection();
    }
}
