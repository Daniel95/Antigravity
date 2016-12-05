using UnityEngine;
using System;
using System.Collections;

public class SpeedMultiplier : MonoBehaviour {

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

    void Awake() {
        velocity = GetComponent<ControlVelocity>();
        velocity.SpeedMultiplier = startMultiplier;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(speedPostiveInput))
        {
            ChangeSpeedMultiplier(changeSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(speedNegativeInput)) {
            ChangeSpeedMultiplier(-changeSpeed * Time.deltaTime);
        }
    }

    public void SetSpeed(float _newMultiplierSpeed) {

        //check if the new multiplier is outside the threshold
        if (Mathf.Abs(_newMultiplierSpeed) >= minSwitchSpeedThreshold)
        {

            //only replace the speed when it is lower or equals the maxSpeed
            if (_newMultiplierSpeed <= maxSpeed && _newMultiplierSpeed >= -maxSpeed)
            {
                velocity.SpeedMultiplier = _newMultiplierSpeed;
            }
        }
        else
        { //round the speed if it reaches the minimal threshold
            if (_newMultiplierSpeed > 0)
            {
                velocity.SpeedMultiplier = -minSwitchSpeedThreshold;
            }
            else
            {
                velocity.SpeedMultiplier = minSwitchSpeedThreshold;
            }

        }

        //secure way of checking if the multiplier has flipped between + and -, even if the change is really fast
        if (originalSpeedMultiplier <= 0 && _newMultiplierSpeed > 0 || originalSpeedMultiplier >= 0 && _newMultiplierSpeed < 0) {

            if (switchedSpeed != null)
                switchedSpeed();
        }

        originalSpeedMultiplier = _newMultiplierSpeed;
    }

    public void SetSpeedMultiplierPercentage(float _percentage)
    {
        float newMultiplierSpeed = _percentage * (maxSpeed * 2) - maxSpeed;

        SetSpeed(newMultiplierSpeed);
    }

    private void ChangeSpeedMultiplier(float _change)
    {
        float _newMultiplierSpeed = originalSpeedMultiplier + _change;

        SetSpeed(_newMultiplierSpeed);
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
