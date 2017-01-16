using UnityEngine;
using System;
using System.Collections;

public class FutureDirectionIndicator : MonoBehaviour {

    [SerializeField]
    private Transform arrowTransform;

    private CharScriptAccess _charAccess;

    private LookAt _lookAt;

    private Vector2 _lookDir;

    private Frames _frames;

    void Start() {
        _lookAt = arrowTransform.GetComponent<LookAt>();
        _charAccess = GetComponent<CharScriptAccess>();
        _frames = GetComponent<Frames>();
        _lookDir = _charAccess.ControlVelocity.GetCeilVelocityDirection();

        SubscribeToDelegates();
    }

    void OnEnable() {
        if (_charAccess != null)
            SubscribeToDelegates();
    }

    void OnDisable()
    {
        if (_charAccess != null) {
            _charAccess.ControlDirection.FinishedDirectionLogic -= PointToControlledDir;
            _charAccess.SpeedMultiplier.SwitchedMultiplier -= PointToCeilVelocityDir;
            _charAccess.ControlTakeOff.TookOff -= PointToCeilVelocityDir;
        }
    }

    void SubscribeToDelegates() {
        _charAccess.ControlDirection.FinishedDirectionLogic += PointToControlledDir;
        _charAccess.SpeedMultiplier.SwitchedMultiplier += PointToCeilVelocityDir;
        _charAccess.ControlTakeOff.TookOff += PointToCeilVelocityDir;
    }

    private void PointToControlledDir(Vector2 _futureDir)
    {

        _lookDir = _futureDir * _charAccess.ControlVelocity.GetMultiplierDir();

        _lookAt.UpdateLookAt((Vector2)transform.position + _lookDir);
    }

    /// <summary>
    /// look at the the ceiled values of our current normalized velocity.
    /// </summary>
    public void PointToCeilVelocityDir() {
        _frames.ExecuteAfterDelay(1, UpdateCeilVelocityDir);
    }

    private void UpdateCeilVelocityDir() {
        Vector2 velocityDir = _charAccess.ControlVelocity.GetCeilVelocityDirection();

        //don't update the dir when its new value is zero
        if (velocityDir.x != 0)
        {
            _lookDir.x = velocityDir.x;
        }
        if (velocityDir.y != 0)
        {
            _lookDir.y = velocityDir.y;
        }

        _lookAt.UpdateLookAt((Vector2)transform.position + _lookDir);
    }

    private void UpdateLookDir(Vector2 _newLookDir)
    {
        //don't update the dir when its new value is zero
        if (_newLookDir.x != 0)
        {
            _lookDir.x = _newLookDir.x;
        }
        if (_newLookDir.y != 0)
        {
            _lookDir.y = _newLookDir.y;
        }
    }
}
