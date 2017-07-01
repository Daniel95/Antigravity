using IoCPlus;
using UnityEngine;

public class PlayerResetVelocityCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    protected override void Execute() {
        playerVelocityRef.Get().SetVelocity(Vector2.zero);
    }
}
