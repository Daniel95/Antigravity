using UnityEngine;
using System;
using System.Collections;

public class SpeedMultiplier : MonoBehaviour, ITriggerer
{
    [SerializeField]
    private float startMultiplier = 1;

    [SerializeField]
    private float flipSpeed = 0.055f;

    [SerializeField]
    private float maxSpeed = 1.2f;

    [SerializeField]
    private float minSwitchSpeedThreshold = 0.1f;

    private CharScriptAccess _charAcces;

    public Action SwitchedMultiplier;

    //the original speed multiplier, not affected by any rules of setSpeed
    private float _originalSpeedMultiplier;

    private Coroutine _moveMultiplier;

    private float _lastTarget;

    //used by action trigger to decide when to stop the instructions/tutorial
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    void Start()
    {
        _charAcces = GetComponent<CharScriptAccess>();

        _charAcces.ControlVelocity.SpeedMultiplier = _originalSpeedMultiplier = startMultiplier;
    }

    /// <summary>
    /// Sets a new speed multiplier, and corrects it if it is outside of bounds using maxSpeed and minSwitchSpeedThreshold.
    /// </summary>
    /// <param name="newMultiplierSpeed"></param>
    public void SetSpeedMultiplier(float newMultiplierSpeed)
    {

        //check if the new multiplier is outside the threshold
        if (Mathf.Abs(newMultiplierSpeed) >= minSwitchSpeedThreshold)
        {
            //only replace the speed when it is lower or equals the maxSpeed
            if (newMultiplierSpeed <= maxSpeed && newMultiplierSpeed >= -maxSpeed)
            {
                _charAcces.ControlVelocity.SpeedMultiplier = newMultiplierSpeed;
            }
        }
        else
        { //round the speed if it reaches the minimal threshold
            if (newMultiplierSpeed > 0)
            {
                _charAcces.ControlVelocity.SpeedMultiplier = minSwitchSpeedThreshold;
            }
            else
            {
                _charAcces.ControlVelocity.SpeedMultiplier = -minSwitchSpeedThreshold;
            }
        }

        //secure way of checking if the multiplier has flipped between pos and neg, even if the change is really fast
        if (_originalSpeedMultiplier <= 0 && newMultiplierSpeed > 0 || _originalSpeedMultiplier >= 0 && newMultiplierSpeed < 0)
        {
            _charAcces.ControlSpeed.TempSpeedIncrease();

            if (SwitchedMultiplier != null)
                SwitchedMultiplier();
        }

        _originalSpeedMultiplier = newMultiplierSpeed;
    }

    public void SetSpeedMultiplierPercentage(float percentage)
    {
        float newMultiplierSpeed = (percentage * (maxSpeed * 2) - maxSpeed);

        SetSpeedMultiplier(newMultiplierSpeed);
    }

    //increases or decreases the multiplier by an set amount
    private void ChangeSpeedMultiplier(float change)
    {
        float _newMultiplierSpeed = _originalSpeedMultiplier + change;

        SetSpeedMultiplier(_newMultiplierSpeed);
    }

    /// <summary>
    /// Smoothly flips the speed multiplier from 1 to -1 or inverted.
    /// </summary>
    public void SmoothFlipMultiplier()
    {
        if (StopTrigger != null)
            StopTrigger();

        float newTarget = _originalSpeedMultiplier * -1;

        if (_moveMultiplier != null)
        {
            StopCoroutine(_moveMultiplier);
            newTarget = _lastTarget *= -1;
        }
        else
        {
            _lastTarget = newTarget;
        }

        _moveMultiplier = StartCoroutine(MoveMultiplier(newTarget, flipSpeed));
    }

    /// <summary>
    /// Resets the multiplier to a positive value.
    /// </summary>
    public void MakeMultiplierPositive()
    {
        if (_originalSpeedMultiplier < 0) {
            if (_moveMultiplier != null)
            {
                _lastTarget = -1;
                SetSpeedMultiplier(Mathf.Abs(_originalSpeedMultiplier * -1));
                SmoothFlipMultiplier();
            }
            else {
                SetSpeedMultiplier(Mathf.Abs(_originalSpeedMultiplier));
            }
        }
    }

    private IEnumerator MoveMultiplier(float target, float time)
    {
        var fixedUpdate = new WaitForFixedUpdate();

        while (Mathf.Round(_originalSpeedMultiplier * 10) / 10 != Mathf.Round(target * 10) / 10)
        {
            SetSpeedMultiplier(Mathf.Lerp(_originalSpeedMultiplier, target, time));
            yield return fixedUpdate;
        }
        SetSpeedMultiplier(target);
    }
}