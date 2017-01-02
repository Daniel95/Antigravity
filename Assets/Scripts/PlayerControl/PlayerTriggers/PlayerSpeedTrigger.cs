using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedTrigger : MonoBehaviour, ITriggerable {

    [SerializeField]
    private ControlVelocity controlVelocity;

    [SerializeField]
    private bool setVelocityToDirection;

    public void TriggerActivate()
    {
        controlVelocity.SetSpeed(0);
    }

    public void TriggerStop()
    {
        if (setVelocityToDirection)
        {
            controlVelocity.SetVelocity(controlVelocity.GetDirection() * controlVelocity.OriginalSpeed);
        }

        controlVelocity.SetSpeed(controlVelocity.OriginalSpeed);
    }
}
