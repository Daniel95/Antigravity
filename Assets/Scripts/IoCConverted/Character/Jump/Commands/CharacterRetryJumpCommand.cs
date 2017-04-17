using IoCPlus;

public class CharacterRetryJumpCommand : Command {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;
    [Inject] private Ref<ICharacterCollisionDirection> characterCollisionDirectionRef;
    [Inject] private Ref<ICharacterRaycast> characterRaycastRef;

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    protected override void Execute() {
        CharacterJumpParameter characterJumpParameter = new CharacterJumpParameter(characterVelocityRef.Get().MoveDirection, 
                                                                                   characterCollisionDirectionRef.Get().GetCurrentCollisionDirection(), 
                                                                                   characterRaycastRef.Get().GetMiddleDirection());

        characterJumpRef.Get().RetryJump(characterJumpParameter);
    }
}
