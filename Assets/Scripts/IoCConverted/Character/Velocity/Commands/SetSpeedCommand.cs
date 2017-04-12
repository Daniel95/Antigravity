using IoCPlus;
using UnityEngine;

public class SetSpeedCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [Inject] private float speed;

    protected override void Execute() {
        controlVelocityRef.Get().SetSpeed(speed);
    }
}
