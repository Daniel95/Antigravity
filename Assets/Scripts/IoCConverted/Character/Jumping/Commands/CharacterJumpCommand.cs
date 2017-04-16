using IoCPlus;
using UnityEngine;

public class CharacterJumpCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [Inject] private CharacterRemoveCollisionDirectionEvent characterRemoveCollisionDirectionEvent;

    [InjectParameter] private CharacterJumpParameter characterJumpParameter;

    protected override void Execute() {
        characterJumpRef.Get().Jump(characterJumpParameter);
        characterRemoveCollisionDirectionEvent.Dispatch(characterJumpParameter.CollisionDirection);
    }
}
