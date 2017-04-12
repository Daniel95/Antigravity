using IoCPlus;
using UnityEngine;

public class EnableDirectionalMovementCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        controlVelocityRef.Get().EnableDirectionalMovement(enable);
    }
}
