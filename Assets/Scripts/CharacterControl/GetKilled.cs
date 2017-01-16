using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GetKilled : MonoBehaviour, IKillable {

    //tags that we should die to
    [SerializeField]
    private string[] deadlyTags;

    public Action Die;

    private bool _inKillerTrigger;

    public void EnteringKillingTrigger(string _killerTag)
    {
        if (CheckDeadliness(_killerTag))
        {
            _inKillerTrigger = true;
        }
    }

    public void ExitingKillingTrigger(string _killerTag)
    {
        if (CheckDeadliness(_killerTag))
        {
            _inKillerTrigger = false;
        }
    }

    //when we collide with something while in the killing trigger, we die
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_inKillerTrigger)
        {
            if (Die != null)
                Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < deadlyTags.Length; i++)
        {
            if (collision.transform.CompareTag(deadlyTags[i]))
            {
                if (Die != null)
                    Die();
            }
        }
    }

    /// <summary>
    /// check if this tag is deadly to us
    /// </summary>
    /// <param name="_killerTag"></param>
    /// <returns></returns>
    private bool CheckDeadliness(string _killerTag)
    {
        for (int i = 0; i < deadlyTags.Length; i++)
        {
            if (_killerTag == deadlyTags[i])
            {
                return true;
            }
        }

        return false;
    }
}
