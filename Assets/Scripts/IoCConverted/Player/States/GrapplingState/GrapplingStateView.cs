﻿using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class GrapplingStateView : View, IGrapplingState, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IGrapplingState> grapplingStateRef;

    [Inject] private UpdateGrapplingStateEvent updateGrapplingStateEvent;

    private Coroutine updateGrapplingStateCoroutine;
    private Vector2 lastVelocity;

    public override void Initialize() {
        grapplingStateRef.Set(this);
        updateGrapplingStateCoroutine = StartCoroutine(UpdateGrapplingState());
    }

    public override void Dispose() {
        StopCoroutine(updateGrapplingStateCoroutine);
        updateGrapplingStateCoroutine = null;
    }

    private IEnumerator UpdateGrapplingState() {
        while(true) {
            updateGrapplingStateEvent.Dispatch();
            yield return null;
        }
    }
}