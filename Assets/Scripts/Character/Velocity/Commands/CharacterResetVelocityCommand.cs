using IoCPlus;
using UnityEngine;

public class CharacterResetVelocityCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterVelocityRef.Get().Velocity = Vector2.zero;
    }
}
