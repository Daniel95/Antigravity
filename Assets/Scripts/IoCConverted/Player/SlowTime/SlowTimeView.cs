using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class SlowTimeView : View, ISlowTime {

    [Inject] private Ref<ISlowTime> slowTimeRef;

    [SerializeField] private float minOffset = 0.025f;
    [SerializeField] private float slowTimeScale = 0.09f;
    [SerializeField] private float slowDownTime = 0.085f;
    [SerializeField] private float returnSpeed = 0.03f;

    private Coroutine moveTimeScaleCoroutine;
    private bool slowTimeActive;
    private Action reachedTarget;

    public override void Initialize() {
        slowTimeRef.Set(this);
    }

    /// <summary>
    /// Starts slowing the time, after timescale reaches its minimum it returns to normal over time.
    /// </summary>
    public void SlowTime() {
        if(!slowTimeActive) {
            if (moveTimeScaleCoroutine != null) {
                StopCoroutine(moveTimeScaleCoroutine);
                moveTimeScaleCoroutine = null;
            }

            slowTimeActive = true;

            reachedTarget += SlowlyReturnToNormal;

            //slow down the time
            moveTimeScaleCoroutine = StartCoroutine(MoveTimeScale(slowTimeScale, slowDownTime));
        }
    }

    /// <summary>
    /// Reset the time to normal.
    /// </summary>
    public void StopSlowTime() {
        if (slowTimeActive) {
            reachedTarget -= SlowlyReturnToNormal;

            StopCoroutine(moveTimeScaleCoroutine);
            moveTimeScaleCoroutine = null;

            slowTimeActive = false;

            Time.timeScale = 1;
        }
    }

    private void SlowlyReturnToNormal() {
        moveTimeScaleCoroutine = StartCoroutine(MoveTimeScale(1, returnSpeed));
        reachedTarget -= SlowlyReturnToNormal;
    }

    private IEnumerator MoveTimeScale(float target, float time) {
        var fixedUpdate = new WaitForFixedUpdate();

        while (Mathf.Abs(Time.timeScale - target) > minOffset) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, target, time);
            yield return fixedUpdate;
        }

        Time.timeScale = target;

        if (reachedTarget != null) {
            reachedTarget();
        }
    }
}
