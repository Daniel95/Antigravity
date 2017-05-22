﻿using IoCPlus;

public class CharacterRetryJumpCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycast> characterRaycastRef;

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    protected override void Execute() {
        CharacterJumpEvent.Parameter characterJumpParameter = new CharacterJumpEvent.Parameter(
            characterVelocityRef.Get().Direction, 
            characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(characterRaycastRef.Get().GetCornersDirection()), 
            characterRaycastRef.Get().GetMiddleDirection()
        );

        characterJumpRef.Get().RetryJump(characterJumpParameter);
    }
}
