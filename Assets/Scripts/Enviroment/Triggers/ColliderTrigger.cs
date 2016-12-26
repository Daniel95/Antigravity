using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : TriggerBase {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            ActivateTriggers();
        }
    }
}
