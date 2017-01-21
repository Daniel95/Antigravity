using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ControlSpeed : MonoBehaviour, IBoostAble {

    [SerializeField]
    private float maxSpeedMultiplier = 5f;

    [SerializeField]
    private float speedChangeDivider = 10f;

    [SerializeField]
    private float maxSpeedChange = 2;

    [SerializeField]
    private float speedBoostValue = 0.25f;

    [SerializeField]
    private float returnSpeed = 0.005f;

    [SerializeField]
    private float standardNeutralValue = 0.5f;

    [SerializeField]
    private int changeSpeedCdStartValue = 60;

    private int _changeSpeedCdCounter = -1;

    private bool _changeSpeedCdIsActive;

    private ControlVelocity _velocity;

    private void Start()
    {
        _velocity = GetComponent<ControlVelocity>();
    }

    /// <summary>
    /// increases the speed with a multplier, then returns it to the original over time
    /// </summary>
    public void TempSpeedIncrease()
    {
        TempSpeedChange(0.5f + speedBoostValue, standardNeutralValue);
    }

    public void SpeedDecrease()
    {
        TempSpeedChange(0.5f - speedBoostValue, standardNeutralValue);
    }


    /// <summary>
    /// Changes speed by a specified amount, below 0.5 is slower, above 0.5 is faster
    /// never sets speed lower then minimum
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="neutralValue"></param>
    public void TempSpeedChange(float amount, float neutralValue)
    {
        if (_changeSpeedCdIsActive)
        {
            return;
        }

        if (_changeSpeedCdCounter < 0)
        {
            StartCoroutine(ChangeSpeedCdCounter());
        }
        else
        {
            _changeSpeedCdCounter = changeSpeedCdStartValue;
        }

        float newSpeed = CalcNewSpeed(amount, neutralValue);

        if (newSpeed - _velocity.CurrentSpeed > maxSpeedChange)
        {
            newSpeed = _velocity.CurrentSpeed + maxSpeedChange;
        }

        //keep the speeds in bounds, above originalspeed, below the speed muliplier
        if (newSpeed > _velocity.OriginalSpeed * maxSpeedMultiplier)
            newSpeed = _velocity.OriginalSpeed * maxSpeedMultiplier;
        else if (newSpeed < _velocity.OriginalSpeed)
            newSpeed = _velocity.OriginalSpeed;

        BoostSpeed(newSpeed, returnSpeed);
    }

    /// <summary>
    /// boosts with a certain amount.
    /// </summary>
    /// <param name="newSpeed"></param>
    /// <param name="myReturnSpeed"></param>
    public void BoostSpeed(float newSpeed, float myReturnSpeed)
    {
        _velocity.SetSpeed(newSpeed);

        _velocity.StartReturnSpeedToOriginal(myReturnSpeed);
    }

    private float CalcNewSpeed(float amount, float neutralValue)
    {
        return _velocity.CurrentSpeed + _velocity.CurrentSpeed * (amount / neutralValue - 1) / (_velocity.CurrentSpeed * speedChangeDivider);
    }

    private IEnumerator ChangeSpeedCdCounter()
    {
        _changeSpeedCdIsActive = true;
        _changeSpeedCdCounter = changeSpeedCdStartValue;

        while (true)
        {
            _changeSpeedCdCounter--;
            if (_changeSpeedCdCounter < 0)
            {
                _changeSpeedCdIsActive = false;
                yield break;
            }


            yield return new WaitForEndOfFrame();
        }
    }
}
