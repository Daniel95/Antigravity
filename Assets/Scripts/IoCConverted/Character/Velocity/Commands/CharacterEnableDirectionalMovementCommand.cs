using IoCPlus;
using UnityEngine;

public class CharacterEnableDirectionalMovementCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [InjectParameter] private bool enable;

    protected override void Execute() {
        controlVelocityRef.Get().EnableDirectionalMovement(enable);
    }
}
