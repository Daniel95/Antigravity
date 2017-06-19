using IoCPlus;
using UnityEngine;

public class PlayerSetVelocityToVelocityDirectionSpeedCommand : Command {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject] private Ref<IHook> hookRef;

    protected override void Execute() {
        Vector2 velocityDirectionSpeed = playerVelocityRef.Get().GetVelocityDirection() * playerVelocityRef.Get().CurrentSpeed;
        playerVelocityRef.Get().Velocity = velocityDirectionSpeed;
    }
}