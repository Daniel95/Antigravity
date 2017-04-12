using IoCPlus;
using UnityEngine;

public class AddVelocityCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [Inject] private Vector2 velocity;

    protected override void Execute() {
        controlVelocityRef.Get().AddVelocity(velocity);
    }
}
