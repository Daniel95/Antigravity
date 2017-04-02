using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : HookTypeBase {

    public Action StartedGrappleLocking;
    public Action StoppedGrappleLocking;

    [SerializeField]
    private float minDistance = 0.75f;

    private Coroutine _grappleAnchorUpdate;

    private DistanceJoint2D _distanceJoint;

    private void Awake()
    {
        _distanceJoint = GetComponent<DistanceJoint2D>();
        _distanceJoint.enabled = false;
    }
    
    /*
    protected override void Hooked(int hookedLayer)
    {
        if (hookedLayer == HookWeapon.HookAbleLayers.GrappleSurface)
        {
            CheckHookDistance();
        }
    }

    private void CheckHookDistance()
    {
        float distance = Vector2.Distance(transform.position, HookProjectileGObj.transform.position);

        if (distance > minDistance)
        {
            EnterGrappleLock(distance);
        }
        else 
        {
            ExitGrappleLock();
        }
    }

    private void EnterGrappleLock(float distance)
    {
        CurrentHookState = HookStates.Active;

        HookProjectileScript.Attached = null;

        if (StopTrigger != null)
        {
            StopTrigger();
        }

        //activate distanceJoint2D
        if (StartedGrappleLocking != null)
        {
            StartedGrappleLocking();
        }

        StartDistaneJoint(distance);

        //check for collisions and update the anchor position of the grappling hook line
        _grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());

        ChangeSpeedByAngle();
    }

    public void ExitGrappleLock()
    {
        Canceled();
    }

    protected override void Canceled()
    {
        if (StoppedGrappleLocking != null)
            StoppedGrappleLocking();

        _distanceJoint.enabled = false;

        if (_grappleAnchorUpdate != null)
           StopCoroutine(_grappleAnchorUpdate);

        base.Canceled();
    }

    /// <summary>
    /// Updates the position of the grappling hook anchors.
    /// Uses spawns a new anchor between when there is something in between the owner and the original owner.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GrappleAnchorUpdate()
    {
        while (true)
        {
            RaycastHit2D hitToAnchor = Physics2D.Linecast(transform.position, Anchors[Anchors.Count - 1].position, RayLayers);

            if (hitToAnchor.collider != null)
            {
                Vector2 anchorPos = hitToAnchor.point + ((Vector2)transform.position - hitToAnchor.point).normalized * 0.1f;

                Anchors.Add(CreateGrappleAnchor(anchorPos, hitToAnchor.transform));

                LineRenderer.numPositions = Anchors.Count + 1;

                _distanceJoint.distance = Vector2.Distance(transform.position, hitToAnchor.point);
            }
            else if (Anchors.Count > 1)
            {
                RaycastHit2D hitToPreviousAnchor = Physics2D.Linecast(transform.position, Anchors[Anchors.Count - 2].position, RayLayers);

                if (hitToPreviousAnchor.collider == null)
                {
                    _distanceJoint.distance = Vector2.Distance(transform.position, Anchors[Anchors.Count - 1].position) + Vector2.Distance(Anchors[Anchors.Count - 1].position, Anchors[Anchors.Count - 2].position);

                    Anchors.RemoveAt(Anchors.Count - 1);

                    LineRenderer.numPositions--;
                }
            }

            //update the pos each frame
            _distanceJoint.connectedAnchor = Anchors[Anchors.Count - 1].position;

            yield return null;
        }
    }

    protected override void DeactivateHook()
    {
        base.DeactivateHook();

        StopDistanceJoint();
    }

    private void StartDistaneJoint(float distance)
    {
         //init the distance joint & line renderer
        _distanceJoint.enabled = true;

        _distanceJoint.connectedAnchor = Anchors[0].position;
        _distanceJoint.distance = distance;
    }

    public void StopDistanceJoint()
    {
        if (_grappleAnchorUpdate != null)
            StopCoroutine(_grappleAnchorUpdate);

        _distanceJoint.enabled = false;
    }
    */

    public void EnterGrappleLock(float distance)
    {

    }

    public void ExitGrappleLock()
    {

    }

    public Vector2 Destination
    {
        get { return transform.position; }
    }
}
