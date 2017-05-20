using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : TriggerBase {

    //activates a trigger when colliding with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.Player))
        {
            ActivateTriggers();
        }
    }
}
