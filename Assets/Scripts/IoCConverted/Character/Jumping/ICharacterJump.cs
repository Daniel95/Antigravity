using UnityEngine;
using System.Collections;

public interface ICharacterJump  {

    void TryJump();
    void Bounce(DirectionParameter directionInfo);
    bool CheckBounce(Collision2D collision);
}
