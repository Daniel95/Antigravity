using UnityEngine;
using System;
using System.Collections;

public class ChangeSpeedMultiplier : MonoBehaviour {

    [SerializeField]
    private KeyCode speedNegativeInput = KeyCode.A;

    [SerializeField]
    private KeyCode speedPostiveInput = KeyCode.D;

    [SerializeField]
    private float startMultiplier = 1;

    [SerializeField]
    private float changeSpeed = 0.01f;

    [SerializeField]
    private float maxSpeed = 2f;

    [SerializeField]
    private float minSwitchSpeedThreshold = 0.1f;

    private ControlVelocity velocity;

    public Action switchedSpeed;

    private float originalSpeedMultiplier = 1;

    void Start() {
        velocity = GetComponent<ControlVelocity>();
        velocity.SpeedMultiplier = startMultiplier;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(speedNegativeInput))
        {
            ChangeSpeed(changeSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(speedPostiveInput)) {
            ChangeSpeed(-changeSpeed * Time.deltaTime);
        }
    }

    private void ChangeSpeed(float _change) {
        if (Mathf.Abs(originalSpeedMultiplier) >= minSwitchSpeedThreshold)
        {
            float newSpeed = originalSpeedMultiplier + _change;

            //only replace the speed when it is lower or equals the maxSpeed
            if (newSpeed <= maxSpeed && newSpeed >= -maxSpeed)
            {
                velocity.SpeedMultiplier = originalSpeedMultiplier = newSpeed;
            }
        }
        else { //flip the speed if it reaches the minimal threshold
            if (originalSpeedMultiplier > 0)
            {
                velocity.SpeedMultiplier = originalSpeedMultiplier = -minSwitchSpeedThreshold;
            }
            else {
                velocity.SpeedMultiplier = originalSpeedMultiplier = minSwitchSpeedThreshold;
            }

            if (switchedSpeed != null)
                switchedSpeed();
        }
    }

    public void ResetSpeedMultiplier()
    {
        if (originalSpeedMultiplier != velocity.SpeedMultiplier)
        {
            changeSpeed *= -1;

            originalSpeedMultiplier = velocity.SpeedMultiplier;
        }
    }

    public float OriginalSpeedMultiplier
    {
        get { return originalSpeedMultiplier; }
    }
}
