using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayColliderTrigger : TriggerBase {

    private OneWayDetection _oneWayDetection;

    private void OnEnable()
    {
        _oneWayDetection = GetComponent<OneWayDetection>();
        _oneWayDetection.DetectedRight += CheckCollider;
    }

    private void OnDisable()
    {
        _oneWayDetection.DetectedRight -= CheckCollider;
    }

    private void CheckCollider(Collider2D collider)
    {
        if (collider.CompareTag(Tags.Player))
        {
            ActivateTriggers();
        }
    }
}
