using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayColliderTrigger : TriggerBase {

    private OneWayDetection oneWayDetection;

    private void OnEnable()
    {
        oneWayDetection = GetComponent<OneWayDetection>();
        oneWayDetection.detectedRight += CheckCollider;
    }

    private void OnDisable()
    {
        oneWayDetection.detectedRight -= CheckCollider;
    }

    private void CheckCollider(Collider2D collider)
    {
        if (collider.CompareTag(Tags.Player))
        {
            ActivateTriggers();
        }
    }
}
