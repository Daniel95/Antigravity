using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInputsTrigger : MonoBehaviour, ITriggerable
{
    [SerializeField]
    private PlayerInputs plrInputs;

    public void TriggerActivate()
    {
        print("start key inputs");
        plrInputs.StartKeyInputs();
    }

    public void TriggerStop()
    {
        plrInputs.StopKeyInputs();
    }
}
