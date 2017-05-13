using IoCPlus;
using UnityEngine;

public class CharacterResetMoveDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterVelocityRef.Get().SetMoveDirection(Vector2.zero);
    }
}
