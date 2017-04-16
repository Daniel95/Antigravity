using UnityEngine;

public interface ICharacterJump  {

    void TryJump(CharacterJumpParameter characterJumpParameter);
    void RetryJump(CharacterJumpParameter characterJumpParameter);
    void Jump(CharacterJumpParameter characterJumpParameter);
    void Bounce(CharacterDirectionParameter directionInfo);
    bool CheckBounce(Collision2D collision);
}
