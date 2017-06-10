using UnityEngine;

public interface IFollowCamera {

    void SetTarget(Transform target);
    void SetCameraBounds(CameraBounds cameraBounds);
}
