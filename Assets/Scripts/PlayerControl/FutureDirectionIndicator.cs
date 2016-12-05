using UnityEngine;
using System;
using System.Collections;

public class FutureDirectionIndicator : MonoBehaviour {

    [SerializeField]
    private Transform arrowTransform;

    private PlayerScriptAccess plrAccess;
    private LookAt lookAt;

    private Vector2 lookDir;

    private Frames frames;

    void Start() {
        lookAt = arrowTransform.GetComponent<LookAt>();
        plrAccess = GetComponent<PlayerScriptAccess>();
        frames = GetComponent<Frames>();
        SubscribeToDelegates();
    }

    void OnEnable() {
        if (plrAccess != null)
            SubscribeToDelegates();
    }

    void SubscribeToDelegates() {
        plrAccess.controlDirection.finishedDirectionLogic += PointToControlledDir;
        plrAccess.speedMultiplier.switchedSpeed += PointToCeilVelocityDir;
        plrAccess.switchGravity.switchedGravity += PointToCeilVelocityDir;
    }

    void OnDisable()
    {
        plrAccess.controlDirection.finishedDirectionLogic -= PointToControlledDir;
        plrAccess.speedMultiplier.switchedSpeed -= PointToCeilVelocityDir;
        plrAccess.switchGravity.switchedGravity -= PointToCeilVelocityDir;
    }

    public void PointToControlledDir(Vector2 _futureDir)
    {
        lookDir = _futureDir;
        lookAt.UpdateLookAt((Vector2)transform.position + _futureDir);
    }

    //look at the the ceiled values of our current normalized velocity 
    public void PointToCeilVelocityDir() {
        frames.ExecuteAfterDelay(1, UpdateCeilVelocityDir);
    }

    private void UpdateCeilVelocityDir() {
        Vector2 CeilVelocityDir = plrAccess.controlVelocity.GetCeilVelocityDirection();

        //don't update the dir when its new value is zero
        if (CeilVelocityDir.x != 0)
        {
            lookDir.x = CeilVelocityDir.x;
        }
        if (CeilVelocityDir.y != 0)
        {
            lookDir.y = CeilVelocityDir.y;
        }

        lookAt.UpdateLookAt((Vector2)transform.position + lookDir);
    }
}
