using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpeed : MonoBehaviour, IBoostAble {

    [SerializeField]
    private float tempSpeedAddedMuliplier = 0.35f;

    [SerializeField]
    private float returnSpeed = 0.1f;

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
        float newSpeed = _velocity.OriginalSpeed * (1 + tempSpeedAddedMuliplier);

        if(newSpeed > _velocity.CurrentSpeed)
        {
            _velocity.SetSpeed(newSpeed);

            _velocity.StartReturnSpeedToOriginal(returnSpeed);
        }
    }

    /// <summary>
    /// decreases the speed with a multplier, then returns it to the original over time
    /// </summary>
    public void TempSpeedDecrease()
    {
        float newSpeed = _velocity.OriginalSpeed * (1 - tempSpeedAddedMuliplier);

        if (newSpeed < _velocity.CurrentSpeed)
        {
            _velocity.SetSpeed(newSpeed);

            _velocity.StartReturnSpeedToOriginal(returnSpeed);
        }
    }


    /// <summary>
    /// boosts with a certain amount.
    /// </summary>
    /// <param name="_newSpeed"></param>
    /// <param name="_returnSpeed"></param>
    public void BoostSpeed(float _newSpeed, float _returnSpeed)
    {
        _velocity.SetSpeed(_newSpeed);

        _velocity.StartReturnSpeedToOriginal(_returnSpeed);
    }
}
