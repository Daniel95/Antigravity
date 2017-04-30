using IoCPlus;

public class GrapplingHookContext : Context {

    protected override void SetBindings() {
        base.SetBindings();

        On<HookProjectileIsAttachedEvent>()
            .Do<AbortIfHookAbleLayerIsNotHookedLayerCommand>(HookableLayers.GrappleSurface)
            .Do<GrapplingHookSetDistanceCommand>()
            .Do<AbortIfHookDistanceIsLowerThenMinimalDistance>()
            .Dispatch<GrapplingHookStartedEvent>()
            .Do<ChangeSpeedByAngleCommand>()
            .Do<GrapplingHookEnterGrappleLockCommand>()
            .OnAbort<DispatchCancelHookEventCommand>();

    }
}

/*
    private void EnterGrappleLock(float distance) {
        //activate distanceJoint2D
        if (StartedGrappleLocking != null) {
            StartedGrappleLocking();
        }

        StartDistaneJoint(distance);

        //check for collisions and update the anchor position of the grappling hook line
        _grappleAnchorUpdate = StartCoroutine(GrappleAnchorUpdate());
    }

    public void ExitGrappleLock() {
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

*/
