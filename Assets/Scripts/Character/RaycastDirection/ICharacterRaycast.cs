using UnityEngine;

public interface ICharacterRaycastDirection {

    RaycastData GetVerticalMiddleRaycastData();
    RaycastData GetHorizontalMiddleRaycastData();
        
    Vector2 GetCenterDirection();
    RaycastData GetCenterRaycastData();

    Vector2 GetCornersDirection();
    RaycastData GetCornersRaycastData();

    RaycastData GetCombinedDirectionAndCenterDistances();

}
