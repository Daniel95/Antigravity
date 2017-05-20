using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : TriggerBase {

    [SerializeField]
    private MonoBehaviour triggererScript;

    private ITriggerer _triggerController;

    private void Awake()
    {
        //save the interface from the selected scriptObject
        _triggerController = triggererScript as ITriggerer;
    }

    private void OnEnable()
    {
        //subscribe to the triggerController

        _triggerController.ActivateTrigger += ActivateTrigger;

        _triggerController.StopTrigger += StopTrigger;
    }

    private void OnDisable()
    {
        _triggerController.ActivateTrigger -= ActivateTrigger;

        _triggerController.StopTrigger -= StopTrigger;
    }

    //activate and stop the triggers

    public void ActivateTrigger()
    {
        ActivateTriggers();
    }

    public void StopTrigger()
    {
        StopTriggers();
    }
}
