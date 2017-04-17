using IoCPlus;
using UnityEngine;

public class CharacterResetMoveDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    protected override void Execute() {
        characterVelocityRef.Get().MoveDirection = Vector2.zero;
    }
}
