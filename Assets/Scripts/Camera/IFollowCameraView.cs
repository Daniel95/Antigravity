using UnityEngine;

public interface IFollowCamera {

    void SetTarget(Transform target);
    void EnableFollowCamera(bool enable);
}
