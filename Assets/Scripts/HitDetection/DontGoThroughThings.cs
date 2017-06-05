using System;
using System.Linq;
using UnityEngine;

public class DontGoThroughThings : MonoBehaviour {
    public enum TriggerTarget {
        None = 0,
        Self = 1,
        Other = 2,
        Both = 3
    }

    [SerializeField] private LayerMask hitLayers = -1;
    [SerializeField] private TriggerTarget triggerTarget = TriggerTarget.Both;

    private float minimumExtent;
    private float sqrMinimumExtent;
    private Vector2 previousPosition;
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponents<Collider2D>().FirstOrDefault();
        if (myCollider == null || myRigidbody == null) {
            Debug.LogError("ProjectileCollisionTrigger2D is missing Collider2D or Rigidbody2D component", this);
            enabled = false;
            return;
        }

        previousPosition = myRigidbody.transform.position;
        minimumExtent = Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    private void FixedUpdate() {
        if (myCollider.enabled) {
            Vector2 movementThisStep = (Vector2)transform.position - previousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > sqrMinimumExtent) {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

                RaycastHit2D hitInfo = Physics2D.Raycast(previousPosition, movementThisStep, movementMagnitude, hitLayers.value);

                if (hitInfo && hitInfo.collider != myCollider) {
                    transform.position = hitInfo.point;
                    if (((int)triggerTarget & (int)TriggerTarget.Other) != 0 && hitInfo.collider.isTrigger) {
                        hitInfo.collider.SendMessage("OnTriggerEnter2D", myCollider, SendMessageOptions.DontRequireReceiver);
                    }
                    if (((int)triggerTarget & (int)TriggerTarget.Self) != 0) {
                        SendMessage("OnTriggerEnter2D", hitInfo.collider, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            previousPosition = transform.position;
        }
    }

    private void OnEnable() {
        previousPosition = transform.position;
    }
}
