using IoCPlus;
using UnityEngine;

public class CharacterSetVelocityCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [InjectParameter] private Vector2 velocity;

    protected override void Execute() {
        characterVelocityRef.Get().Velocity = velocity;
    }
}
