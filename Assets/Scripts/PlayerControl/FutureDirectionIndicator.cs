using UnityEngine;
using System;
using System.Collections;

public class FutureDirectionIndicator : MonoBehaviour {

    [SerializeField]
    private Transform arrowTransform;

    private PlayerScriptAccess plrAccess;
    private LookAt lookAt;

    private Vector2 lookDir;

    void Awake() {
        lookAt = arrowTransform.GetComponent<LookAt>();
        plrAccess = GetComponent<PlayerScriptAccess>();
    }

    void OnEnable() {
        plrAccess.controlDirection.finishedDirectionLogic += PointToControlledDir;
        plrAccess.changeSpeedMultiplier.switchedSpeed += PointToRoundedVelocityDir;
        plrAccess.switchGravity.switchedGravity += PointToRoundedVelocityDir;
    }

    void OnDisable()
    {
        plrAccess.controlDirection.finishedDirectionLogic -= PointToControlledDir;
        plrAccess.changeSpeedMultiplier.switchedSpeed -= PointToRoundedVelocityDir;
        plrAccess.switchGravity.switchedGravity += PointToRoundedVelocityDir;
    }

    public void PointToControlledDir(Vector2 _futureDir)
    {
        lookDir = _futureDir;
        lookAt.UpdateLookAt((Vector2)transform.position + _futureDir);
    }

    public void PointToRoundedVelocityDir() {
        StartCoroutine(WaitOneFrame(UpdateLookAtRoundedVelocityDir));
    }

    IEnumerator WaitOneFrame(Action _action) {
        yield return new WaitForFixedUpdate();
        _action();
    }

    void UpdateLookAtRoundedVelocityDir() {
        Vector2 CeilVelocityDir = plrAccess.controlVelocity.GetCeilVelocityDirection();

        if (CeilVelocityDir.x != 0) {
            lookDir.x = CeilVelocityDir.x;
        }
        if (CeilVelocityDir.y != 0) {
            lookDir.y = CeilVelocityDir.y;
        }

        lookAt.UpdateLookAt((Vector2)transform.position + lookDir);
    }
}
