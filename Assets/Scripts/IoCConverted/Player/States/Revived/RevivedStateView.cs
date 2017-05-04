using UnityEngine;
using System.Collections;
using System;
using IoCPlus;

public class RevivedStateView : View, IRevivedState, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public bool IsReadyForLaunchInput { get { return isReadyForLaunchInput; } set { isReadyForLaunchInput = value; } }
    public bool IsInPosition { get { return isInPosition; } set { isInPosition = value; } }

    private Ref<IRevivedState> revivedStateRef;

    [SerializeField] private GameObject gun;
    [SerializeField] private int launchDelayWhenRespawning = 20;

    private bool isReadyForLaunchInput;
    private bool isInPosition;

    public override void Initialize() {
        revivedStateRef.Set(this);
        StartCoroutine(DelayLaunchingInput());
    }

    public override void Dispose() {
        base.Dispose();

        if (StopTrigger != null) {
            StopTrigger();
        }
    }

    public void StartDelayLaunchInput() {
        StartCoroutine(DelayLaunchingInput());
    }

    IEnumerator DelayLaunchingInput() {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !isInPosition) {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        isReadyForLaunchInput = true;
    }
}