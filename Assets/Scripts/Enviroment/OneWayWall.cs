using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour {

    [SerializeField]
    private string defaultLayer = "Default";

    [SerializeField]
    private string ignoreRaycastLayer = "Ignore Raycast";

    [SerializeField]
    private Collider2D ourCollider;

    private OneWayDetection oneWayDetection;

    private void OnEnable()
    {
        oneWayDetection = GetComponent<OneWayDetection>();
        oneWayDetection.detectedRight += DetectedRight;
        oneWayDetection.detectedLeft += DetectedLeft;
    }

    private void OnDisable()
    {
        oneWayDetection.detectedRight -= DetectedRight;
        oneWayDetection.detectedLeft -= DetectedLeft;
    }

    private void DetectedRight(Collider2D collider)
    {
        Physics2D.IgnoreCollision(ourCollider, collider, false);
        gameObject.layer = LayerMask.NameToLayer(defaultLayer);
    }

    private void DetectedLeft(Collider2D collider)
    {
        Physics2D.IgnoreCollision(ourCollider, collider, true);
        gameObject.layer = LayerMask.NameToLayer(ignoreRaycastLayer);
    }
}
