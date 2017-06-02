﻿using UnityEngine;

public interface ICharacterRaycastDirection {

    int GetVerticalCornersDirection();
    int GetHorizontalCornersDirection();
    int GetVerticalMiddleDirection();
    int GetHorizontalMiddleDirection();
    Vector2 GetMiddleDirection();
    Vector2 GetCornersDirection();
}
