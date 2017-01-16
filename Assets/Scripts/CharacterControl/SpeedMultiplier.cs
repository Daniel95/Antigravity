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

    private ControlVelocity _velocity;

    public Action SwitchedMultiplier;

    //the original speed multiplier, not affected by any rules of setSpeed
    private float _originalSpeedMultiplier;

    private Coroutine _moveMultiplier;

    private float _lastTarget;

    //used by action trigger to decide when to stop the instructions/tutorial
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    void Start()
    {
        _velocity = GetComponent<ControlVelocity>();
        _velocity.SpeedMultiplier = _originalSpeedMultiplier = startMultiplier;
    }

    /// <summary>
    /// Sets a new speed multiplier, and corrects it if it is outside of bounds using maxSpeed and minSwitchSpeedThreshold.
    /// </summary>
    /// <param name="_newMultiplierSpeed"></param>
    public void SetSpeedMultiplier(float _newMultiplierSpeed)
    {

        //check if the new multiplier is outside the threshold
        if (Mathf.Abs(_newMultiplierSpeed) >= minSwitchSpeedThreshold)
        {
            //only replace the speed when it is lower or equals the maxSpeed
            if (_newMultiplierSpeed <= maxSpeed && _newMultiplierSpeed >= -maxSpeed)
            {
                _velocity.SpeedMultiplier = _newMultiplierSpeed;
            }
        }
        else
        { //round the speed if it reaches the minimal threshold
            if (_newMultiplierSpeed > 0)
            {
                _velocity.SpeedMultiplier = minSwitchSpeedThreshold;
            }
            else
            {
                _velocity.SpeedMultiplier = -minSwitchSpeedThreshold;
            }
        }

        //secure way of checking if the multiplier has flipped between pos and neg, even if the change is really fast
        if (_originalSpeedMultiplier <= 0 && _newMultiplierSpeed > 0 || _originalSpeedMultiplier >= 0 && _newMultiplierSpeed < 0)
        {
            if (SwitchedMultiplier != null)
                SwitchedMultiplier();
        }

        _originalSpeedMultiplier = _newMultiplierSpeed;
    }

    public void SetSpeedMultiplierPercentage(float _percentage)
    {
        float newMultiplierSpeed = (_percentage * (maxSpeed * 2) - maxSpeed);

        SetSpeedMultiplier(newMultiplierSpeed);
    }

    //increases or decreases the multiplier by an set amount
    private void ChangeSpeedMultiplier(float _change)
    {
        float _newMultiplierSpeed = _originalSpeedMultiplier + _change;

        SetSpeedMultiplier(_newMultiplierSpeed);
    }

    /// <summary>
    /// Smoothly flips the speed multiplier from 1 to -1 or inverted.
    /// </summary>
    public void SmoothFlipMultiplier()
    {
        if (stopTrigger != null)
            stopTrigger();

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

    IEnumerator MoveMultiplier(float _target, float _time)
    {
        while (Mathf.Round(_originalSpeedMultiplier * 10) / 10 != Mathf.Round(_target * 10) / 10)
        {
            SetSpeedMultiplier(Mathf.Lerp(_originalSpeedMultiplier, _target, _time));
            yield return new WaitForFixedUpdate();
        }
        SetSpeedMultiplier(_target);
    }
}