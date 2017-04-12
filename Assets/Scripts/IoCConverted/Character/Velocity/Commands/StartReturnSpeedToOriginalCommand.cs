using IoCPlus;
using UnityEngine;

public class StartReturnSpeedToOriginalCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [Inject] private float returnSpeed;

    protected override void Execute() {
        controlVelocityRef.Get().StartReturnSpeedToOriginal(returnSpeed);
    }
}
