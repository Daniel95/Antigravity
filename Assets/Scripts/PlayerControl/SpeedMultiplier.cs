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
    private float flipSpeed = 0.01f;

    [SerializeField]
    private float maxSpeed = 2f;

    [SerializeField]
    private float minSwitchSpeedThreshold = 0.1f;

    private ControlVelocity velocity;

    public Action switchedMultiplier;

    private float originalSpeedMultiplier = 1;

    private float flippedCompensation = 1;

    private Coroutine moveMultiplier;

    private float lastTarget;

    void Awake() {
        velocity = GetComponent<ControlVelocity>();
        velocity.SpeedMultiplier = startMultiplier;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            SmoothFlipMultiplier();
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
                velocity.SpeedMultiplier = minSwitchSpeedThreshold;
            }
            else
            {
                velocity.SpeedMultiplier = -minSwitchSpeedThreshold;
            }
        }

        //secure way of checking if the multiplier has flipped between + and -, even if the change is really fast
        if (originalSpeedMultiplier <= 0 && _newMultiplierSpeed > 0 || originalSpeedMultiplier >= 0 && _newMultiplierSpeed < 0) {
            if (switchedMultiplier != null)
                switchedMultiplier();
        }

        originalSpeedMultiplier = _newMultiplierSpeed;
    }

    public void SetSpeedMultiplierPercentage(float _percentage)
    {
        float newMultiplierSpeed = (_percentage * (maxSpeed * 2) - maxSpeed) * flippedCompensation;

        SetSpeed(newMultiplierSpeed);
    }

    //increases or decreases the multiplier by an set amount
    private void ChangeSpeedMultiplier(float _change)
    {
        float _newMultiplierSpeed = originalSpeedMultiplier + _change * flippedCompensation;

        SetSpeed(_newMultiplierSpeed);
    }

    public void SmoothFlipMultiplier() {
        float newTarget = originalSpeedMultiplier * -1;

        if (moveMultiplier != null)
        {
            StopCoroutine(moveMultiplier);
            newTarget = lastTarget *= -1;
        }
        else {
            lastTarget = newTarget;
        }

        moveMultiplier = StartCoroutine(MoveMultiplier(newTarget, flipSpeed));
    }

    public void ResetSpeedMultiplier()
    {
        if (originalSpeedMultiplier != velocity.SpeedMultiplier)
        {
            print("flip multiplier");

            flippedCompensation *= -1;

            originalSpeedMultiplier = velocity.SpeedMultiplier;
        }
    }

    public float OriginalSpeedMultiplier
    {
        get { return originalSpeedMultiplier; }
    }

    IEnumerator MoveMultiplier(float _target, float _time)
    {
        while (Mathf.Round(originalSpeedMultiplier * 10) / 10 != Mathf.Round(_target * 10) / 10)
        {
            SetSpeed(Mathf.Lerp(originalSpeedMultiplier, _target, _time));
            yield return new WaitForFixedUpdate();
        }
        originalSpeedMultiplier = _target;
    }
}
