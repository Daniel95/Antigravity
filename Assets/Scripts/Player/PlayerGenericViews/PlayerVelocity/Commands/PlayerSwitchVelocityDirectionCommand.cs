using IoCPlus;
using UnityEngine;

public class PlayerSwitchVelocityDirectionCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SwitchVelocityDirection();
    }
}
