using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HookProjectile : MonoBehaviour {

    public Action<int> Attached;

    public Action Returned;

    public Action Canceled;

    private MoveTowards _moveTowards;

    private Frames _frames;

    private Transform _attachedTransform;

    private int _returnPointsIndex;

    private List<Vector2> _returnPoints = new List<Vector2>();

    private int _hookedLayer;

    void Awake() {
        _moveTowards = GetComponent<MoveTowards>();
        _frames = GetComponent<Frames>();
    }

    public void GoToShootPos(Vector2 destination) {
        _hookedLayer = 0;

        _moveTowards.ReachedDestination = ReachedShootPos;
        _moveTowards.StartMoving(destination);
    }

    public void Return(List<Vector2> returnPoints)
    {
        _attachedTransform = null;
        _hookedLayer = 0;
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
            _moveTowards.ReachedDestination = Returned;
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
        if (_hookedLayer != 0)
        {
            transform.SetParent(_attachedTransform);

            if (Attached != null)
                Attached(_hookedLayer);
        }
        else
        {
            if (Canceled != null)
                Canceled();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == HookWeapon.HookAbleLayers.GrappleSurface)
        {
            _attachedTransform = collision.transform;
            _hookedLayer = HookWeapon.HookAbleLayers.GrappleSurface;
        }
        else if (collision.gameObject.layer == HookWeapon.HookAbleLayers.PullSurface)
        {
            _hookedLayer = HookWeapon.HookAbleLayers.PullSurface;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == HookWeapon.HookAbleLayers.GrappleSurface || collision.gameObject.layer == HookWeapon.HookAbleLayers.PullSurface)
        {
            _hookedLayer = 0;
        }
    }
}
