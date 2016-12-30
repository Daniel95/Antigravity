using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisions : MonoBehaviour {

    private List<Collider2D> currentlyCollidingTriggers = new List<Collider2D>();

    //when a trigger enters a non-trigger collider
    public Action<Collider2D> onTriggerEnterCollision;

    //when a trigger enters a trigger collider
    public Action<Collider2D> onTriggerEnterTrigger;

    //when a trigger exits a non-trigger collider
    public Action<Collider2D> onTriggerExitCollision;

    //when a trigger exits a trigger collider
    public Action<Collider2D> onTriggerExitTrigger;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (onTriggerEnterCollision != null)
                onTriggerEnterCollision(collider);
        }
        else if(!CheckAlreadyColliding(collider))
        {
            if (onTriggerEnterTrigger != null)
                onTriggerEnterTrigger(collider);

            currentlyCollidingTriggers.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (onTriggerExitCollision != null)
                onTriggerExitCollision(collider);
        }
        else if(CheckAlreadyColliding(collider))
        {
            if (onTriggerExitTrigger != null)
                onTriggerExitTrigger(collider);

            currentlyCollidingTriggers.Remove(collider);
        }
    }

    private bool CheckAlreadyColliding(Collider2D _collider)
    {
        return currentlyCollidingTriggers.Contains(_collider);
    }
}
