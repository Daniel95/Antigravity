using IoCPlus;
using UnityEngine;

public class CharacterAddVelocityCommand : Command {

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [Inject] private Vector2 velocity;

    protected override void Execute() {
        controlVelocityRef.Get().AddVelocity(velocity);
    }
}
