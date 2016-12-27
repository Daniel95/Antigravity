using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GetKilled : MonoBehaviour, IKillable {

    public Action die;

    private bool inKillerTrigger;

    public void EnteringKillingTrigger()
    {
        inKillerTrigger = true;
    }

    public void ExitingKillingTrigger()
    {
        inKillerTrigger = false;
    }

    //when we collide with something while in the killing trigger, we die
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (inKillerTrigger)
        {
            if(die != null)
                die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.transform.CompareTag(Tags.Killer))
        {
            if (die != null)
                die();
        }
    }
}
