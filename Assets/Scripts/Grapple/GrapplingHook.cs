using UnityEngine;
using System;
using System.Collections;

public class GrapplingHook : MonoBehaviour, IWeapon {

    [SerializeField]
    private GameObject grappleProjectile;

    private DistanceJoint2D distanceJoint;

    private LineRenderer lineRenderer;

    private GameObject currentGrappleProjectile;

    public Action StartedGrappleLocking;

    public Action ExitedGrappleLocking;

    private Coroutine lineUpdateCoroutine;

    //use the direction to determine which direction we go when we lands
    private Vector2 currentDirection;


    void Start() {
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint.enabled = false;
    }

    //spawns the grapple projectile and activates its moveTowards script
    public void Fire(Vector2 _direction, Vector2 _destination, Vector2 _spawnPosition) {
        if (currentGrappleProjectile != null)
        {
            ExitGrappleLock();
        }

        currentDirection = _direction;

        currentGrappleProjectile = Instantiate(grappleProjectile, _spawnPosition, new Quaternion(0, 0, 0, 0)) as GameObject;

        MoveTowards grappleMovement = currentGrappleProjectile.GetComponent<MoveTowards>();
        grappleMovement.reachedDestination += EnterGrappleLock;
        grappleMovement.StartMoving(_destination);

        lineRenderer.enabled = true;
        lineUpdateCoroutine = StartCoroutine(UpdateLineRendererPositions());
    }

    IEnumerator UpdateLineRendererPositions() {
        while (true) {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentGrappleProjectile.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }

    private void EnterGrappleLock()
    {;
        if (StartedGrappleLocking != null)
            StartedGrappleLocking();

        distanceJoint.enabled = true;
        
        distanceJoint.connectedAnchor = currentGrappleProjectile.transform.position;
        distanceJoint.distance = Vector2.Distance(transform.position, currentGrappleProjectile.transform.position);
    }

    public void ExitGrappleLock() {

        if (ExitedGrappleLocking != null)
            ExitedGrappleLocking();

        lineRenderer.enabled = false;
        StopCoroutine(lineUpdateCoroutine);
        distanceJoint.enabled = false;
        Destroy(currentGrappleProjectile);
    }

    public Vector2 Direction
    {
        get { return currentDirection; }
    }

    public Vector2 Destination
    {
        get { return currentGrappleProjectile.transform.position; }
    }
}
