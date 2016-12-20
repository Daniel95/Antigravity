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
        lookDir = plrAccess.controlVelocity.GetCeilVelocityDirection();

        SubscribeToDelegates();
    }

    void OnEnable() {
        if (plrAccess != null)
            SubscribeToDelegates();
    }

    void OnDisable()
    {
        if (plrAccess != null) {
            plrAccess.controlDirection.finishedDirectionLogic -= PointToControlledDir;
            plrAccess.speedMultiplier.switchedMultiplier -= PointToCeilVelocityDir;
            plrAccess.switchGravity.switchedGravity -= PointToCeilVelocityDir;
        }
    }

    void SubscribeToDelegates() {
        plrAccess.controlDirection.finishedDirectionLogic += PointToControlledDir;
        plrAccess.speedMultiplier.switchedMultiplier += PointToCeilVelocityDir;
        plrAccess.switchGravity.switchedGravity += PointToCeilVelocityDir;
    }

    public void PointToControlledDir(Vector2 _futureDir)
    {

        lookDir = _futureDir * plrAccess.controlVelocity.GetMultiplierDir();

        lookAt.UpdateLookAt((Vector2)transform.position + lookDir);
    }
    
    //look at the the ceiled values of our current normalized velocity 
    public void PointToCeilVelocityDir() {
        frames.ExecuteAfterDelay(1, UpdateCeilVelocityDir);
    }

    private void UpdateCeilVelocityDir() {
        Vector2 velocityDir = plrAccess.controlVelocity.GetCeilVelocityDirection();

        //don't update the dir when its new value is zero
        if (velocityDir.x != 0)
        {
            lookDir.x = velocityDir.x;
        }
        if (velocityDir.y != 0)
        {
            lookDir.y = velocityDir.y;
        }

        lookAt.UpdateLookAt((Vector2)transform.position + lookDir);
    }

    private void UpdateLookDir(Vector2 _newLookDir)
    {
        //don't update the dir when its new value is zero
        if (_newLookDir.x != 0)
        {
            lookDir.x = _newLookDir.x;
        }
        if (_newLookDir.y != 0)
        {
            lookDir.y = _newLookDir.y;
        }
    }
}
