using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {

    //tells the IKillable interface when it is in the killingTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable killAble = collision.gameObject.GetComponent<IKillable>();

        if (killAble != null)
        {
            killAble.EnteringKillingTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IKillable killAble = collision.gameObject.GetComponent<IKillable>();

        if (killAble != null)
        {
            killAble.ExitingKillingTrigger();
        }
    }
}
