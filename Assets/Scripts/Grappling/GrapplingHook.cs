﻿using UnityEngine;
using System;
using System.Collections;

public class GrapplingHook : MonoBehaviour, IWeapon {

    [SerializeField]
    private GameObject grappleProjectilePrefab;

    [SerializeField]
    private float minDistance = 0.3f;

    //variables to control the unity components
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;

    //to save our LineUpdateCoroutine;
    private Coroutine lineUpdateCoroutine;
    private Coroutine holdGrappleCoroutine;

    //other scripts can subscribe to know when the grapple locks and unlocks
    public Action StartedGrappleLocking;
    public Action StoppedGrappleLocking;

    //our current grapple data
    private GameObject grappleProjectile;
    private MoveTowards grappleMovement;

    private enum GrapplingHookStates { busyShooting, busyHolding, busyPullingBack, activate, inactive };

    private GrapplingHookStates currentGrapplingHookState = GrapplingHookStates.inactive;

    //use the direction to determine which direction we go when we lands
    private Vector2 currentDirection;

    void Start() {
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint.enabled = false;

        grappleProjectile = Instantiate(grappleProjectilePrefab, Vector2.zero, new Quaternion(0, 0, 0, 0)) as GameObject;
        grappleMovement = grappleProjectile.GetComponent<MoveTowards>();
        grappleProjectile.SetActive(false);
    }

    //spawns the grapple projectile and activates its moveTowards script
    public void Fire(Vector2 _direction, Vector2 _destination, Vector2 _spawnPosition) {

        if (currentGrapplingHookState == GrapplingHookStates.inactive)
        {
            ShootGrappleHook(_direction, _destination, _spawnPosition);
        }
        //if we still have a grapple activate, deactivate it first before we shoot a new one
        else if (currentGrapplingHookState == GrapplingHookStates.activate || currentGrapplingHookState == GrapplingHookStates.busyShooting)
        {
            //currentGrapplingHookState = GrapplingHookStates.busyHolding;
            holdGrappleCoroutine = StartCoroutine(HoldGrapple(_direction, _destination, _spawnPosition));
            PullBack();
        } 
    }

    IEnumerator HoldGrapple(Vector2 _direction, Vector2 _destination, Vector2 _spawnPosition) {
        while (currentGrapplingHookState != GrapplingHookStates.inactive) {
            yield return null;
        }

        ShootGrappleHook(_direction, _destination, _spawnPosition);
    }

    private void ShootGrappleHook(Vector2 _direction, Vector2 _destination, Vector2 _spawnPosition) {
        currentGrapplingHookState = GrapplingHookStates.busyShooting;

        currentDirection = _direction;

        grappleProjectile.SetActive(true);
        grappleProjectile.transform.position = _spawnPosition;

        //activate line renderer
        lineRenderer.enabled = true;
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());

        grappleMovement.reachedDestination = EnterGrappleLock;
        grappleMovement.StartMoving(_destination);
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grappleProjectile.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }

    private void EnterGrappleLock()
    {
        float distance = Vector2.Distance(transform.position, grappleProjectile.transform.position);
        //only start grappleLocking when the distance is above the minimal distance
        if (distance > minDistance)
        {
            currentGrapplingHookState = GrapplingHookStates.activate;

            grappleMovement.reachedDestination -= EnterGrappleLock;

            //activate distanceJoint2D
            if (StartedGrappleLocking != null)
                StartedGrappleLocking();

            distanceJoint.enabled = true;

            distanceJoint.connectedAnchor = grappleProjectile.transform.position;
            distanceJoint.distance = distance;
        }
        else {
            //else we exit the grapplehook and pull it back to the player
            ExitGrappleLock();
        }
    }

    //used to cancel grapple locking if we are currently locked, also cancels the grapplingstate
    public void ExitGrappleLock() {

        if (StoppedGrappleLocking != null)
            StoppedGrappleLocking();

        grappleMovement.reachedDestination -= EnterGrappleLock;
        distanceJoint.enabled = false;

        if(holdGrappleCoroutine != null)
            StopCoroutine(holdGrappleCoroutine);

        PullBack();
    }

    //pulls the grappling hook back to the player, once it reached the player destroy it
    void PullBack() {
        if (currentGrapplingHookState != GrapplingHookStates.busyPullingBack)
        {
            currentGrapplingHookState = GrapplingHookStates.busyPullingBack;

            grappleMovement.reachedDestination = ReachedPlayer;
            grappleMovement.StartMoving(transform.position);
        }
    }

    void ReachedPlayer() {
        grappleMovement.reachedDestination -= ReachedPlayer;
        DestroyGrappleLock();
    }

    void DestroyGrappleLock()
    {
        currentGrapplingHookState = GrapplingHookStates.inactive;
        StopLineRenderer();
        grappleProjectile.SetActive(false);
    }

    void StopLineRenderer() {
        lineRenderer.enabled = false;
        StopCoroutine(lineUpdateCoroutine);
    }

    public Vector2 Direction
    {
        get { return currentDirection; }
    }

    public Vector2 Destination
    {
        get { return grappleProjectile.transform.position; }
    }
}
