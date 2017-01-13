using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedTrigger : MonoBehaviour, ITriggerable {

    public bool triggered { get; set; }

    [SerializeField]
    private ControlVelocity controlVelocity;

    private Vector2 lastDirection;

    public void TriggerActivate()
    {
        lastDirection = controlVelocity.GetVelocityDirection();
        controlVelocity.SetSpeed(0);
    }

    public void TriggerStop()
    {
        controlVelocity.SetDirection(lastDirection);

        controlVelocity.SetVelocity(lastDirection * controlVelocity.OriginalSpeed);

        controlVelocity.SetSpeed(controlVelocity.OriginalSpeed);
    }
}
