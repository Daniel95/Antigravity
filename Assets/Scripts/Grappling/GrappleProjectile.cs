using UnityEngine;
using System;
using System.Collections;

public class GrappleProjectile : MonoBehaviour {

    [SerializeField]
    private LayerMask hookAbleLayers;

    public Action reachedDestination;

    public Action grappleCanceled;

    private MoveTowards moveTowards;

    private Frames frames;

    private bool hookedToSurface;

    private Transform moveAbleObject;

    private Vector2 offsetToMoveAble;

    private Coroutine followMovingObject;

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
        if (followMovingObject != null)
            StopCoroutine(followMovingObject);

        moveAbleObject = null;

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
            //if we collided with a moveAble object, make sure the grappleProjectile follows the moving object
            if(CollidedWithMoveAble())
            {
                followMovingObject = StartCoroutine(FollowMovingObject());
            }

            if (reachedDestination != null)
                reachedDestination();
        }
        else
        {
            if (grappleCanceled != null)
                grappleCanceled();
        }
    }

    IEnumerator FollowMovingObject()
    {
        offsetToMoveAble = transform.position - moveAbleObject.transform.position;

        while (true)
        {
            transform.position = (Vector2)moveAbleObject.transform.position + offsetToMoveAble;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we hit a moveAble object, save the offset and 
        if (collision.transform.CompareTag(Tags.MoveAble))
        {
            moveAbleObject = collision.transform;
        }

        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0) {
            hookedToSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.MoveAble))
        {
            moveAbleObject = null;
        }

        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0)
        {
            hookedToSurface = false;
        }
    }

    public bool CollidedWithMoveAble()
    {
        return moveAbleObject != null;
    }
}
