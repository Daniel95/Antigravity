using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzeTime : MonoBehaviour, ITriggerable {

    public bool triggered { get; set; }

    private TimeManagement timeManagement;

    private void Start()
    {
        timeManagement = GetComponent<TimeManagement>();
    }

    public void TriggerActivate()
    {
        timeManagement.SetTimeScale(0);
    }

    public void TriggerStop()
    {
        timeManagement.SetTimeScale(1);
    }
}
