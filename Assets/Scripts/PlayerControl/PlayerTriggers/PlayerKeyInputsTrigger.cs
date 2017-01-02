using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInputsTrigger : MonoBehaviour, ITriggerable
{
    public bool triggered { get; set; }

    [SerializeField]
    private PlayerInputs plrInputs;

    public void TriggerActivate()
    {
        plrInputs.StartKeyInputs();
    }

    public void TriggerStop()
    {
        plrInputs.StopKeyInputs();
    }
}
