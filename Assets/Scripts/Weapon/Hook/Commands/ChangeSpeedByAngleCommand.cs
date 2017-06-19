using IoCPlus;
using UnityEngine;

public class PlayerChangeSpeedByAngleCommand : Command {

    [Inject] private PlayerStatus playerStatus;

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject] private Ref<IHook> hookRef;
    [Inject] private Ref<IHookProjectile> hookProjectileRef;

    [Inject] private PlayerTemporarySpeedChangeEvent playerTemporarySpeedChangeEvent;

    protected override void Execute() {
        float angleDifference = Mathf.Abs(Vector2.Dot(
            (hookProjectileRef.Get().Transform.position - playerStatus.Player.transform.position).normalized,
            playerVelocityRef.Get().GetVelocityDirection()
        ));

        float speedChange = angleDifference * -1 + 1;

        playerTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(speedChange, hookRef.Get().DirectionSpeedNeutralValue));
    }
}
