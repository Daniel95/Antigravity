using UnityEngine;

public interface ICharacterSurroundingDirection {

    Vector2 GetSurroundingsDirection(bool countCollisionDirection = true, bool countRaycastMiddleDirection = true, bool countRaycastCornerDirection = true);
}
