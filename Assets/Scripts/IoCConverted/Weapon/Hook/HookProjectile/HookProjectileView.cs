using IoCPlus;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectileView : View, IHookProjectile {

    public Transform AttachedTransform { get { return attachedTransform; } set { attachedTransform = value; } }
    public int HookedLayerIndex { get { return hookedLayer; } set { hookedLayer = value; } }

    public Action<int> Attached;
    public Action Returned;
    public Action Canceled;

    private Transform attachedTransform;
    private int hookedLayer;
    private MoveTowardsView moveTowards;
    private Frames frames;
    private int returnPointsIndex;
    private List<Vector2> returnPoints = new List<Vector2>();

    void Awake() {
        moveTowards = GetComponent<MoveTowardsView>();
        frames = GetComponent<Frames>();
    }

    public void GoToDestination(Vector2 destination) {
        moveTowards.ReachedDestination = ReachedShootPos;
        moveTowards.StartMoving(destination);
    }

    public void Return(List<Vector2> returnPoints) {
        attachedTransform = null;
        hookedLayer = 0;
        transform.SetParent(null);

        this.returnPoints = returnPoints;

        returnPointsIndex = 1;
        GoToNextPoint();
    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent, true);
    }

    private void GoToNextPoint() {
        //there is a chance we immediatly reach our destination when we startMoving, so we need to increase _returnPointsIndex before we start moving, but we want to use the old value.
        Vector2 nextPoint = returnPoints[returnPointsIndex];
        returnPointsIndex++;

        if (returnPointsIndex >= returnPoints.Count - 1) {
            moveTowards.ReachedDestination = Returned;
        } else {
            moveTowards.ReachedDestination = GoToNextPoint;
        }

        moveTowards.StartMoving(nextPoint);
    }

    private void ReachedShootPos() {
        frames.ExecuteAfterDelay(1, CheckIfHooked);
    }

    public void CheckIfHooked() {
        if (hookedLayer != 0) {
            transform.SetParent(attachedTransform);

            if (Attached != null)
                Attached(hookedLayer);
        }
        else {
            if (Canceled != null)
                Canceled();
        }
    }

}
