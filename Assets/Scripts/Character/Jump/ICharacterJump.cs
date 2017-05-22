public interface ICharacterJump  {

    void TryJump(CharacterJumpEvent.Parameter characterJumpParameter);
    void RetryJump(CharacterJumpEvent.Parameter characterJumpParameter);
    void Jump(CharacterJumpEvent.Parameter characterJumpParameter);
}
