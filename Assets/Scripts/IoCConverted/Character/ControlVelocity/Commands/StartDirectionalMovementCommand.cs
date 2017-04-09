using IoCPlus;
using UnityEngine;

public class StartDirectionalMovementCommand : Command {

    [Inject] private Ref<IControlVelocity> controlVelocityRef;

    protected override void Execute() {
        controlVelocityRef.Get().StartDirectionalMovement();
    }
}
