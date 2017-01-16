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

    private OneWayDetection _oneWayDetection;

    private void OnEnable()
    {
        _oneWayDetection = GetComponent<OneWayDetection>();
        _oneWayDetection.DetectedRight += DetectedRight;
        _oneWayDetection.DetectedLeft += DetectedLeft;
    }

    private void OnDisable()
    {
        _oneWayDetection.DetectedRight -= DetectedRight;
        _oneWayDetection.DetectedLeft -= DetectedLeft;
    }

    private void DetectedRight(Collider2D collision)
    {
        Physics2D.IgnoreCollision(ourCollider, collision, false);
        gameObject.layer = LayerMask.NameToLayer(defaultLayer);
    }

    private void DetectedLeft(Collider2D collision)
    {
        Physics2D.IgnoreCollision(ourCollider, collision, true);
        gameObject.layer = LayerMask.NameToLayer(ignoreRaycastLayer);
    }
}
