using UnityEngine;
using System;

public class GrappleProjectile : MonoBehaviour {

    [SerializeField]
    private LayerMask hookAbleLayers;

    public Action reachedDestination;

    public Action grappleCanceled;

    private MoveTowards moveTowards;

    private Frames frames;

    private bool hookedToSurface;

    void Awake() {
        moveTowards = GetComponent<MoveTowards>();
        frames = GetComponent<Frames>();
    }

    public void GoToShootPos(Vector2 _destination) {
        hookedToSurface = false;

        moveTowards.reachedDestination = ReachedShootPos;
        moveTowards.StartMoving(_destination);
    }

    public void Return(Vector2 _destination)
    {
        moveTowards.reachedDestination = reachedDestination;
        moveTowards.StartMoving(_destination);
    }

    void OnDisable()
    {
        reachedDestination = null;
    }

    private void ReachedShootPos()
    {
        frames.ExecuteAfterDelay(1, CheckIfHooked);
    }

    private void CheckIfHooked()
    {
        if (hookedToSurface)
        {
            if (reachedDestination != null)
                reachedDestination();
        }
        else
        {
            if (grappleCanceled != null)
                grappleCanceled();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0) {
            hookedToSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0)
        {
            hookedToSurface = false;
        }
    }
}
