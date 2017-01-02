using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootControlsTrigger : MonoBehaviour, ITriggerable
{
    [SerializeField]
    private PlayerInputs plrInputs;

    public void TriggerActivate()
    {
        plrInputs.StartShootInputs();
    }

    public void TriggerStop()
    {
        plrInputs.StopShootInputs();
    }
}
