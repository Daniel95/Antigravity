using IoCPlus;
using UnityEngine;

public class CharacterRetryJumpCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycast> characterRaycastRef;

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    protected override void Execute() {
        CharacterJumpParameter characterJumpParameter = new CharacterJumpParameter(characterVelocityRef.Get().Direction, 
                                                                                   characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(), 
                                                                                   characterRaycastRef.Get().GetMiddleDirection());

        Vector2 collisionDirection = characterCollisionDirectionRef.Get().GetCurrentCollisionDirection();
        Vector2 raycastDirection = characterRaycastRef.Get().GetMiddleDirection();

        //if collisiondir is zero, it may be because we are barely not colliding, while it looks like we are.
        //as a backup plan we use raycasting if this happens so we can still jump
        if (characterJumpParameter.CollisionDirection == Vector2.zero) {
            characterJumpParameter.CollisionDirection = raycastDirection;
        }

        characterJumpRef.Get().RetryJump(characterJumpParameter);
    }
}
