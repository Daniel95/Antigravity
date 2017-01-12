using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateSlowTime : MonoBehaviour {

    private SlowTime slowTime;

    private PlayerInputs playerInputs;

    private void Awake()
    {
        slowTime = GetComponent<SlowTime>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    public void SetSlowTimeInput(bool _input)
    {
        if (_input)
        {
            playerInputs.tappedExpired += StartSlowTime;
            playerInputs.release += StopSlowTime;
        }
        else
        {
            playerInputs.tappedExpired -= StartSlowTime;
            playerInputs.release -= StopSlowTime;
        }
    }

    private void StartSlowTime()
    {
        if (!slowTime.SlowTimeActive)
            slowTime.StartSlowTime();
    }

    private void StopSlowTime()
    {
        if (slowTime.SlowTimeActive)
            slowTime.StopSlowTime();
    }
}
