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

    private Transform _attachedTransform;

    void Awake() {
        _moveTowards = GetComponent<MoveTowards>();
        _frames = GetComponent<Frames>();
    }

    public void GoToShootPos(Vector2 destination) {
        _hookedToSurface = false;

        _moveTowards.ReachedDestination = ReachedShootPos;
        _moveTowards.StartMoving(destination);
    }

    public void Return(Vector2 destination)
    {
        _attachedTransform = null;

        _hookedToSurface = false;

        _moveTowards.ReachedDestination = ReachedDestination;
        _moveTowards.StartMoving(destination);

        transform.SetParent(null);
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
            transform.SetParent(_attachedTransform);

            if (ReachedDestination != null)
                ReachedDestination();
        }
        else
        {
            if (GrappleCanceled != null)
                GrappleCanceled();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0) {
            _attachedTransform = collision.transform;
            _hookedToSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hookAbleLayers) != 0)
        {
            _hookedToSurface = false;
        }
    }
}
