using UnityEngine;
using System;
using System.Collections;

public class SpeedMultiplier : MonoBehaviour, ITriggerer
{
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

    //the original speed multiplier, not affected by any rules of setSpeed
    private float originalSpeedMultiplier;

    private Coroutine moveMultiplier;

    private float lastTarget;

    //used by action trigger to decide when to stop the instructions/tutorial
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    void Start()
    {
        velocity = GetComponent<ControlVelocity>();
        velocity.SpeedMultiplier = originalSpeedMultiplier = startMultiplier;
    }

    public void SetSpeedMultiplier(float _newMultiplierSpeed)
    {

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

        //secure way of checking if the multiplier has flipped between pos and neg, even if the change is really fast
        if (originalSpeedMultiplier <= 0 && _newMultiplierSpeed > 0 || originalSpeedMultiplier >= 0 && _newMultiplierSpeed < 0)
        {
            if (switchedMultiplier != null)
                switchedMultiplier();
        }

        originalSpeedMultiplier = _newMultiplierSpeed;
    }

    public void SetSpeedMultiplierPercentage(float _percentage)
    {
        float newMultiplierSpeed = (_percentage * (maxSpeed * 2) - maxSpeed);

        SetSpeedMultiplier(newMultiplierSpeed);
    }

    //increases or decreases the multiplier by an set amount
    private void ChangeSpeedMultiplier(float _change)
    {
        float _newMultiplierSpeed = originalSpeedMultiplier + _change;

        SetSpeedMultiplier(_newMultiplierSpeed);
    }

    public void SmoothFlipMultiplier()
    {
        if (stopTrigger != null)
            stopTrigger();

        float newTarget = originalSpeedMultiplier * -1;

        if (moveMultiplier != null)
        {
            StopCoroutine(moveMultiplier);
            newTarget = lastTarget *= -1;
        }
        else
        {
            lastTarget = newTarget;
        }

        moveMultiplier = StartCoroutine(MoveMultiplier(newTarget, flipSpeed));
    }

    public void MakeMultiplierPositive()
    {
        if (originalSpeedMultiplier < 0) {
            if (moveMultiplier != null)
            {
                lastTarget = -1;
                SetSpeedMultiplier(Mathf.Abs(originalSpeedMultiplier * -1));
                SmoothFlipMultiplier();
            }
            else {
                SetSpeedMultiplier(Mathf.Abs(originalSpeedMultiplier));
            }
        }
    }

    IEnumerator MoveMultiplier(float _target, float _time)
    {
        while (Mathf.Round(originalSpeedMultiplier * 10) / 10 != Mathf.Round(_target * 10) / 10)
        {
            SetSpeedMultiplier(Mathf.Lerp(originalSpeedMultiplier, _target, _time));
            yield return new WaitForFixedUpdate();
        }
        SetSpeedMultiplier(_target);
    }
}