using UnityEngine;
using System;

public class GrappleProjectile : MonoBehaviour, IShootable {

    public Action grappleLocked;

    private MoveTowards moveTowards;

    void Start() {
        moveTowards = GetComponent<MoveTowards>();
    }

    public void SetDestination(Vector2 _destination) {
        moveTowards.StartMoving(_destination);
        moveTowards.reachedDestination += grappleLocked;
    }

    void OnDestroy()
    {
        grappleLocked = null;
    }
}
