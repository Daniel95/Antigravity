using IoCPlus;
using UnityEngine;

public class CharacterSetMoveDirectionCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [InjectParameter] private Vector2 direction;

    protected override void Execute() {
        Debug.Log(direction);
        characterVelocityRef.Get().MoveDirection = direction;
    }
}
