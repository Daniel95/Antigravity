using UnityEngine;
using System;
using System.Collections;

public class FutureDirectionIndicator : MonoBehaviour {

    [SerializeField]
    private Transform arrowTransform;

    private CharScriptAccess charAccess;
    private LookAt lookAt;

    private Vector2 lookDir;

    private Frames frames;

    void Start() {
        lookAt = arrowTransform.GetComponent<LookAt>();
        charAccess = GetComponent<CharScriptAccess>();
        frames = GetComponent<Frames>();
        lookDir = charAccess.ControlVelocity.GetCeilVelocityDirection();

        SubscribeToDelegates();
    }

    void OnEnable() {
        if (charAccess != null)
            SubscribeToDelegates();
    }

    void OnDisable()
    {
        if (charAccess != null) {
            charAccess.ControlDirection.FinishedDirectionLogic -= PointToControlledDir;
            charAccess.SpeedMultiplier.SwitchedMultiplier -= PointToCeilVelocityDir;
            charAccess.ControlTakeOff.TookOff -= PointToCeilVelocityDir;
        }
    }

    void SubscribeToDelegates() {
        charAccess.ControlDirection.FinishedDirectionLogic += PointToControlledDir;
        charAccess.SpeedMultiplier.SwitchedMultiplier += PointToCeilVelocityDir;
        charAccess.ControlTakeOff.TookOff += PointToCeilVelocityDir;
    }

    public void PointToControlledDir(Vector2 _futureDir)
    {

        lookDir = _futureDir * charAccess.ControlVelocity.GetMultiplierDir();

        lookAt.UpdateLookAt((Vector2)transform.position + lookDir);
    }
    
    //look at the the ceiled values of our current normalized velocity 
    public void PointToCeilVelocityDir() {
        frames.ExecuteAfterDelay(1, UpdateCeilVelocityDir);
    }

    private void UpdateCeilVelocityDir() {
        Vector2 velocityDir = charAccess.ControlVelocity.GetCeilVelocityDirection();

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
