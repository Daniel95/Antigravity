using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRay : MonoBehaviour {

    [SerializeField]
    private LineRenderer line;

    private Vector2 rayDestination;

    private Coroutine updateLineRendererPositions;

    private bool aimRayActive;

    void Start()
    {
        line.enabled = false;
    }

    public void StartAimRay(Vector2 _destination)
    {
        aimRayActive = true;

        //activate the line renderer
        line.enabled = true;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, _destination);

        updateLineRendererPositions = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions()
    {
        while (true)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, rayDestination);
            yield return null;
        }
    }

    public void StopAimRay()
    {
        if (aimRayActive)
        {
            StopCoroutine(updateLineRendererPositions);

            aimRayActive = false;
            line.enabled = false;
        }
    }

    public Vector2 SetRayDestination
    {
        set { rayDestination = value; }
    }

    public bool AimRayActive
    {
        get { return aimRayActive; }
    }
}
