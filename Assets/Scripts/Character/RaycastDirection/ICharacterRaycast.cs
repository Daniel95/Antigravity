using UnityEngine;

public interface ICharacterRaycastDirection {

    int GetVerticalMiddleDirection();
    int GetHorizontalMiddleDirection();
    Vector2 GetCenterDirection();
    Vector2 GetCornersDirection();
}
