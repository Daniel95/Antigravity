using UnityEngine;

public interface ICharacterRaycast {

    int GetVerticalCornersDirection();
    int GetHorizontalCornersDirection();
    int GetVerticalMiddleDirection();
    int GetHorizontalMiddleDirection();
    Vector2 GetMiddleDirection();
}
