using IoCPlus;
using UnityEngine;

public class PlayerAddVelocityCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [Inject] private Vector2 velocity;

    protected override void Execute() {
        playerVelocityRef.Get().AddVelocity(velocity);
    }
}
