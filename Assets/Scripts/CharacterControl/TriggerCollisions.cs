using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisions : MonoBehaviour {

    private List<Collider2D> _currentlyCollidingTriggers = new List<Collider2D>();

    //when a trigger enters a non-trigger collider
    public Action<Collider2D> OnTriggerEnterCollision;

    //when a trigger enters a trigger collider
    public Action<Collider2D> OnTriggerEnterTrigger;

    //when a trigger exits a non-trigger collider
    public Action<Collider2D> OnTriggerExitCollision;

    //when a trigger exits a trigger collider
    public Action<Collider2D> OnTriggerExitTrigger;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (OnTriggerEnterCollision != null)
                OnTriggerEnterCollision(collider);
        }
        else if(!CheckAlreadyColliding(collider))
        {
            if (OnTriggerEnterTrigger != null)
                OnTriggerEnterTrigger(collider);

            _currentlyCollidingTriggers.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            if (OnTriggerExitCollision != null)
                OnTriggerExitCollision(collider);
        }
        else if(CheckAlreadyColliding(collider))
        {
            if (OnTriggerExitTrigger != null)
                OnTriggerExitTrigger(collider);

            _currentlyCollidingTriggers.Remove(collider);
        }
    }

    private bool CheckAlreadyColliding(Collider2D _collider)
    {
        return _currentlyCollidingTriggers.Contains(_collider);
    }
}
