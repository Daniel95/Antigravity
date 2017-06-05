using UnityEngine;

interface IGrapplingHook {

    DistanceJoint2D DistanceJoint { get; set; }

    void StartGrappleLock(float distanceJointDistance);
    void StopGrappleLock();
}
