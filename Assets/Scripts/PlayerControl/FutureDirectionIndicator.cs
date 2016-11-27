using UnityEngine;
using System.Collections;

public class FutureDirectionIndicator : MonoBehaviour {

    [SerializeField]
    private Transform arrowTransform;

    private ControlDirection controlDir;
    private LookAt lookAt;

    void Awake() {
        controlDir = GetComponent<ControlDirection>();
        lookAt = arrowTransform.GetComponent<LookAt>();
    }

    void OnEnable() {
        controlDir.finishedDirectionLogic += PointToFutureDir;
    }

    void OnDisable()
    {
        controlDir.finishedDirectionLogic -= PointToFutureDir;
    }

    void PointToFutureDir(Vector2 _futureDir)
    {
        lookAt.UpdateLookAt((Vector2)transform.position + _futureDir);
    }
}
