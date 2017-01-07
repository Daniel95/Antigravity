using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpeed : MonoBehaviour, IBoostAble {

    [SerializeField]
    private float tempSpeedAddedMuliplier = 0.35f;

    [SerializeField]
    private float returnSpeed = 0.1f;

    private ControlVelocity velocity;

    private void Start()
    {
        velocity = GetComponent<ControlVelocity>();
    }

    //increases the speed with a multplier, then returns it to the original over time
    public void TempSpeedIncrease()
    {
        float newSpeed = velocity.OriginalSpeed * (1 + tempSpeedAddedMuliplier);

        if(newSpeed > velocity.CurrentSpeed)
        {
            velocity.SetSpeed(newSpeed);

            velocity.StartReturnSpeedToOriginal(returnSpeed);
        }
    }

    //decreases the speed with a multplier, then returns it to the original over time
    public void TempSpeedDecrease()
    {
        float newSpeed = velocity.OriginalSpeed * (1 - tempSpeedAddedMuliplier);

        if (newSpeed < velocity.CurrentSpeed)
        {
            velocity.SetSpeed(newSpeed);

            velocity.StartReturnSpeedToOriginal(returnSpeed);
        }
    }

    public void BoostSpeed(float _newSpeed, float _returnSpeed)
    {
        velocity.SetSpeed(_newSpeed);

        velocity.StartReturnSpeedToOriginal(_returnSpeed);
    }
}
