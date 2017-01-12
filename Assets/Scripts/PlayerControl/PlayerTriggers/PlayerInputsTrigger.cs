using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsTrigger : MonoBehaviour, ITriggerable
{
    public bool triggered { get; set; }

    [SerializeField]
    private PlayerInputs plrInputs;

    public void TriggerActivate()
    {
        plrInputs.SetInputs(true);
    }

    public void TriggerStop()
    {
        plrInputs.SetInputs(false);
    }
}
