using UnityEngine;

public interface ICharacterSliding {

    Transform CurrentTargetCornerTransform { get; }

    void StartCheckingRotateAroundCornerConditions(Transform targetCornerTransform);
    void StopCheckingRotateAroundCornerConditions();

}
    