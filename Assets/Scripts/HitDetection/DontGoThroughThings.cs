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

    void Awake() {
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

    void FixedUpdate() {
        Vector2 originalPosition = transform.position;
        Vector2 movementThisStep = (Vector2)transform.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent) {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

            RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(previousPosition, movementThisStep, movementMagnitude, hitLayers.value);

            for (int i = 0; i < hitsInfo.Length; ++i) {
                RaycastHit2D hitInfo = hitsInfo[i];
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
        }

        previousPosition = transform.position = originalPosition;
    }
}
