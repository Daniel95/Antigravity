using UnityEngine;
using System;
using System.Collections;

public class GrappleProjectile : MonoBehaviour {

    [SerializeField]
    private LayerMask hookAbleLayers;

    public Action ReachedDestination;

    public Action GrappleCanceled;

    private MoveTowards _moveTowards;

    private Frames _frames;

    private bool _hookedToSurface;

    private Transform _moveAbleObject;

    private Vector2 _offsetToMoveAble;

    private Coroutine _followMovingObject;

    void Awake() {
        _moveTowards = GetComponent<MoveTowards>();
        _frames = GetComponent<Frames>();
    }

    public void GoToShootPos(Vector2 _destination) {
        _hookedToSurface = false;

        _moveTowards.ReachedDestination = ReachedShootPos;
        _moveTowards.StartMoving(_destination);
    }

    public void Return(Vector2 _destination)
    {
        if (_followMovingObject != null)
            StopCoroutine(_followMovingObject);

        _moveAbleObject = null;

        _moveTowards.ReachedDestination = ReachedDestination;
        _moveTowards.StartMoving(_destination);
    }

    void OnDisable()
    {
        ReachedDestination = null;
    }

    private void ReachedShootPos()
    {
        _frames.ExecuteAfterDelay(1, CheckIfHooked);
    }

    private void CheckIfHooked()
    {
        if (_hookedToSurface)
        {
            //if we collided with a moveAble object, make sure the grappleProjectile follows the moving object
            if(CollidedWithMoveAble())
            {
                _followMovingObject = StartCoroutine(FollowMovingObject());
            }

            if (ReachedDestination != null)
                ReachedDestination();
        }
        else
        {
            if (GrappleCanceled != null)
                GrappleCanceled();
        }
    }

    IEnumerator FollowMovingObject()
    {
        _offsetToMoveAble = transform.position - _moveAbleObject.transform.position;

        while (true)
        {
            transform.position = (Vector2)_moveAbleObject.transform.position + _offsetToMoveAble;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we hit a moveAble object, save the offset and 
        if (collision.transform.CompareTag(Tags.MoveAble))
        {
            _moveAbleObject = collision.transform;
        }

        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0) {
            _hookedToSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.MoveAble))
        {
            _moveAbleObject = null;
        }

        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0)
        {
            _hookedToSurface = false;
        }
    }

    public bool CollidedWithMoveAble()
    {
        return _moveAbleObject != null;
    }
}
