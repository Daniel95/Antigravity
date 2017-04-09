using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
public class GrapplingHookView : View, IGrapplingHook {
    
    [Inject] private HookModel hookModel;

    [Inject] private Ref<IGrapplingHook> grapplingHook;

    [Inject] private GrapplingHookStartedEvent startedGrapplingEvent;
    [Inject] private ChangeSpeedByAngleEvent changeSpeedByAngleEvent;
    [Inject] private AddAnchorEvent addAnchorEvent;

    public Action StartedGrappleLocking;
    public Action StoppedGrappleLocking;

    [SerializeField] private float minDistance = 0.75f;
    private DistanceJoint2D distanceJoint;

    private Coroutine _grappleAnchorUpdate;

    private void Awake() {
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }

    public override void Initialize() {
        grapplingHook.Set(this);
    }

    public void Hooked(int hookedLayer)
    {
        if (hookedLayer == HookWeapon.HookAbleLayers.GrappleSurface)
        {
            CheckHookDistance();
        }
    }

    private void CheckHookDistance()
    {
        float distance = Vector2.Distance(transform.position, hookModel.HookProjectileGameObject.transform.position);

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
        hookModel.CurrentHookState = HookModel.HookStates.Active;

        hookModel.HookProjectile.Attached = null;

        startedGrapplingEvent.Dispatch();

        //if (StopTrigger != null)
        //{
        //    StopTrigger();
        //}

        //activate distanceJoint2D
        if (StartedGrappleLocking != null)
        {
            StartedGrappleLocking();
        }

        StartDistaneJoint(distance);

        //check for collisions and update the anchor position of the grappling hook line
        _grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());

        changeSpeedByAngleEvent.Dispatch();
    }

    public void ExitGrappleLock()
    {
        Canceled();
    }

    public void Canceled()
    {
        if (StoppedGrappleLocking != null)
            StoppedGrappleLocking();

        distanceJoint.enabled = false;

        if (_grappleAnchorUpdate != null)
           StopCoroutine(_grappleAnchorUpdate);
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
            RaycastHit2D hitToAnchor = Physics2D.Linecast(transform.position, hookModel.Anchors[hookModel.Anchors.Count - 1].position, hookModel.RayLayers);

            if (hitToAnchor.collider != null)
            {
                Vector2 anchorPos = hitToAnchor.point + ((Vector2)transform.position - hitToAnchor.point).normalized * 0.1f;

                addAnchorEvent.Dispatch(anchorPos, hitToAnchor.transform);

                hookModel.LineRendererComponent.positionCount = hookModel.Anchors.Count + 1;

                distanceJoint.distance = Vector2.Distance(transform.position, hitToAnchor.point);
            }
            else if (hookModel.Anchors.Count > 1)
            {
                RaycastHit2D hitToPreviousAnchor = Physics2D.Linecast(transform.position, hookModel.Anchors[hookModel.Anchors.Count - 2].position, hookModel.RayLayers);

                if (hitToPreviousAnchor.collider == null)
                {
                    distanceJoint.distance = Vector2.Distance(transform.position, hookModel.Anchors[hookModel.Anchors.Count - 1].position) + Vector2.Distance(hookModel.Anchors[hookModel.Anchors.Count - 1].position, hookModel.Anchors[hookModel.Anchors.Count - 2].position);

                    hookModel.Anchors.RemoveAt(hookModel.Anchors.Count - 1);

                    hookModel.LineRendererComponent.positionCount--;
                }
            }

            //update the pos each frame
            distanceJoint.connectedAnchor = hookModel.Anchors[hookModel.Anchors.Count - 1].position;

            yield return null;
        }
    }

    private void DeactivateHook()
    {
        StopDistanceJoint();
    }

    private void StartDistaneJoint(float distance)
    {
         //init the distance joint & line renderer
        distanceJoint.enabled = true;

        distanceJoint.connectedAnchor = hookModel.Anchors[0].position;
        distanceJoint.distance = distance;
    }

    public void StopDistanceJoint()
    {
        if (_grappleAnchorUpdate != null)
            StopCoroutine(_grappleAnchorUpdate);

        distanceJoint.enabled = false;
    }

}
 