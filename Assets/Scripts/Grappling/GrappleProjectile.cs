using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GrappleProjectile : MonoBehaviour {

    [SerializeField]
    private LayerMask hookAbleLayers;

    public Action ReachedDestination;

    public Action GrappleCanceled;

    private MoveTowards _moveTowards;

    private Frames _frames;

    private bool _hookedToSurface;

    private Transform _attachedTransform;

    private int _returnPointsIndex;

    private List<Vector2> _returnPoints = new List<Vector2>();

    void Awake() {
        _moveTowards = GetComponent<MoveTowards>();
        _frames = GetComponent<Frames>();
    }

    public void GoToShootPos(Vector2 destination) {
        _hookedToSurface = false;

        _moveTowards.ReachedDestination = ReachedShootPos;
        _moveTowards.StartMoving(destination);
    }

    public void Return(List<Vector2> returnPoints)
    {
        _attachedTransform = null;
        _hookedToSurface = false;
        transform.SetParent(null);

        _returnPoints = returnPoints;

        _returnPointsIndex = 1;
        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        //there is a chance we immediatly reach our destination when we startMoving, so we need to increase _returnPointsIndex before we start moving, but we want to use the old value.
        Vector2 nextPoint = _returnPoints[_returnPointsIndex];
        _returnPointsIndex++;

        if (_returnPointsIndex >= _returnPoints.Count - 1)
        {
            _moveTowards.ReachedDestination = ReachedDestination;
        }
        else
        {
            _moveTowards.ReachedDestination = GoToNextPoint;
        }

        _moveTowards.StartMoving(nextPoint);
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
