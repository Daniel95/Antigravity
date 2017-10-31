using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class PlayerGrapplingView : View, IPlayerGrappling {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<IPlayerGrappling> grapplingStateRef;

    [Inject] private UpdatePlayerGrapplingEvent updateGrapplingStateEvent;

    private Coroutine updateGrapplingStateCoroutine;
    private Vector2 lastVelocity;

    public override void Initialize() {
        grapplingStateRef.Set(this);
    }

    public void StartUpdateGrapplingState() {

        updateGrapplingStateCoroutine = StartCoroutine(UpdateGrapplingState());
    }

    public void StopUpdateGrapplingState() {
        if(updateGrapplingStateCoroutine == null) { return; }
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
