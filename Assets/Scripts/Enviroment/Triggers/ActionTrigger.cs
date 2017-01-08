using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : TriggerBase {

    [SerializeField]
    private MonoBehaviour triggererScript;

    private ITriggerer triggerController;

    private void Awake()
    {
        //save the interface from the selected scriptObject
        triggerController = triggererScript as ITriggerer;
    }

    private void OnEnable()
    {
        //subscribe to the triggerController

        triggerController.activateTrigger += ActivateTrigger;

        triggerController.stopTrigger += StopTrigger;
    }

    private void OnDisable()
    {
        triggerController.activateTrigger -= ActivateTrigger;

        triggerController.stopTrigger -= StopTrigger;
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
