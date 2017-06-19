using IoCPlus;
using UnityEngine;

public class PlayerSetSpeedCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [Inject] private float speed;

    protected override void Execute() {
        playerVelocityRef.Get().SetSpeed(speed);
    }
}
