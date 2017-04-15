using UnityEngine;

public interface ICharacterJump  {

    void TryJump();
    void Jump(CharacterJumpParameter characterJumpParameter);
    void Bounce(DirectionParameter directionInfo);
    bool CheckBounce(Collision2D collision);
}
