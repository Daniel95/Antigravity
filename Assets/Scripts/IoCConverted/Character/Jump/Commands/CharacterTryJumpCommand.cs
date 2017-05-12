using IoCPlus;
using UnityEngine;

public class CharacterTryJumpCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycast> characterRaycastRef;

    [Inject] private Ref<ICharacterJump> characterJumpRef;



    protected override void Execute() {
        CharacterJumpEvent.Parameter characterJumpParameter = new CharacterJumpEvent.Parameter(characterVelocityRef.Get().MoveDirection, 
                                                                                   characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(), 
                                                                                   characterRaycastRef.Get().GetMiddleDirection());

        characterJumpRef.Get().TryJump(characterJumpParameter);
    }
}
