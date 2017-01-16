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
            playerInputs.TappedExpired += StartSlowTime;
            playerInputs.Release += StopSlowTime;
        }
        else
        {
            playerInputs.TappedExpired -= StartSlowTime;
            playerInputs.Release -= StopSlowTime;
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
