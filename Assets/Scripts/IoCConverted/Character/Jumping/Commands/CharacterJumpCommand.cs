using IoCPlus;
using UnityEngine;

public class CharacterJumpCommand : Command {

    [Inject] private Ref<ICharacterJump> characterJumpRef;
    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycasting> characterRaycastingRef;

    protected override void Execute() {
        Vector2 raycastDirection = new Vector2(characterRaycastingRef.Get().GetHorizontalMiddleDirection(), characterRaycastingRef.Get().GetVerticalMiddleDirection());

        characterJumpRef.Get().Jump(new CharacterJumpParameter(characterVelocityRef.Get().Direction, 
                                                               characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(), 
                                                               raycastDirection));

        characterRemoveCollisionDirectionEvent.Dispatch(characterJumpParameter.CollisionDirection);

    }
}
