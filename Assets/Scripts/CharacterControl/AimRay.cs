using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRayView : MonoBehaviour {

    [SerializeField]
    private LineRenderer line;

    private Vector2 _rayDestination;

    private Coroutine _updateLineRendererPositions;

    private bool _aimRayActive;

    void Start()
    {
        line.enabled = false;
    }

    /// <summary>
    /// enables a ray visible ray that can be controlled with RayDestination.
    /// </summary>
    /// <param name="_destination"></param>
    public void StartAimRay(Vector2 _destination)
    {
        _aimRayActive = true;

        //activate the line renderer
        line.enabled = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, _destination);

        _updateLineRendererPositions = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions()
    {
        while (true)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, _rayDestination);
            yield return null;
        }
    }

    /// <summary>
    /// stops the ray.
    /// </summary>
    public void StopAimRay()
    {
        if (_aimRayActive)
        {
            StopCoroutine(_updateLineRendererPositions);

            _aimRayActive = false;
            line.enabled = false;
        }
    }

    /// <summary>
    /// control the destination of the ray.
    /// </summary>
    public Vector2 RayDestination
    {
        set { _rayDestination = value; }
    }

    /// <summary>
    /// check if the ray is already active.
    /// </summary>
    public bool AimRayActive
    {
        get { return _aimRayActive; }
    }
}
