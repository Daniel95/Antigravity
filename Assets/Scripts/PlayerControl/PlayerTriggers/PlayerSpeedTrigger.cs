using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedTrigger : MonoBehaviour, ITriggerable {

    public bool Triggered { get; set; }

    [SerializeField]
    private ControlVelocityView controlVelocity;

    private Vector2 _lastDirection;

    public void TriggerActivate()
    {
        _lastDirection = controlVelocity.GetVelocityDirection();
        controlVelocity.SetSpeed(0);
    }

    public void TriggerStop()
    {
        controlVelocity.SetDirection(_lastDirection);

        controlVelocity.SetVelocity(_lastDirection * controlVelocity.OriginalSpeed);

        controlVelocity.SetSpeed(controlVelocity.OriginalSpeed);
    }
}
