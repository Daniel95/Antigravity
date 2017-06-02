using IoCPlus;
using UnityEngine;

public class CharacterTryJumpCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycastDirection> characterRaycastRef;

    [Inject] private Ref<ICharacterJump> characterJumpRef;



    protected override void Execute() {
        CharacterJumpEvent.Parameter characterJumpParameter = new CharacterJumpEvent.Parameter(characterVelocityRef.Get().Direction, 
                                                                                   characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(), 
                                                                                   characterRaycastRef.Get().GetMiddleDirection());

        characterJumpRef.Get().TryJump(characterJumpParameter);
    }
}
