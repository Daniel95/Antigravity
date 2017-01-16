using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzeTime : MonoBehaviour, ITriggerable {

    public bool Triggered { get; set; }

    private TimeManagement _timeManagement;

    private void Start()
    {
        _timeManagement = GetComponent<TimeManagement>();
    }

    public void TriggerActivate()
    {
        _timeManagement.SetTimeScale(0);
    }

    public void TriggerStop()
    {
        _timeManagement.SetTimeScale(1);
    }
}
