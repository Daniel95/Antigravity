using IoCPlus;
using UnityEngine;

public class PlayerSetVelocityCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;

    [InjectParameter] private Vector2 velocity;

    protected override void Execute() {
        playerVelocityRef.Get().Velocity = velocity;
    }
}
