using UnityEngine;

public interface ICharacterRotateAroundCorner {

    Transform CurrentTargetCornerTransform { get; }

    void StartCheckingRotateAroundCornerTransformConditions(Transform targetCornerTransform, Vector2 moveDirection);
    void StopCheckingRotateAroundCornerTransformConditions(Transform targetCornerTransform);
    void StopAllCheckingRotateAroundCornerConditions();
    void CancelRotatingAroundCorner();

}
    