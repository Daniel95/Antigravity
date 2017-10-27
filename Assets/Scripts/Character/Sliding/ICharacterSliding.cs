using UnityEngine;

public interface ICharacterSliding  {

    void StartCheckingRotateAroundCornerConditions(Vector2 position);
    void StopCheckingRotateAroundCornerConditions();

}
    