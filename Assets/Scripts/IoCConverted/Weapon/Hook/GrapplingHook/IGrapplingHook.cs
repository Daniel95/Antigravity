using UnityEngine;

interface IGrapplingHook {

    DistanceJoint2D DistanceJoint { get; set; }
    float HookDistance { get; set; }

    void StartGrappleLock();
    void StopGrappleLock();
}
