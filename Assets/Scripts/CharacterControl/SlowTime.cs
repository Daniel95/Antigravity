using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour {

    [SerializeField]
    private float minOffset = 0.01f;

    [SerializeField]
    private float slowTimeScale = 0.1f;

    [SerializeField]
    private float slowDownTime = 0.1f;

    [SerializeField]
    private float returnSpeed = 0.01f;

    private Coroutine moveTimeScale;

    private bool slowTimeActive;

    private Action reachedTarget;

    public void StartSlowTime()
    {
        if (moveTimeScale != null)
        {
            StopCoroutine(moveTimeScale);
        }

        slowTimeActive = true;

        reachedTarget += SlowlyReturnToNormal;

        //slow down the time
        moveTimeScale = StartCoroutine(MoveTimeScale(slowTimeScale, slowDownTime));
    }

    private void SlowlyReturnToNormal()
    {
        moveTimeScale = StartCoroutine(MoveTimeScale(1, returnSpeed));
        reachedTarget -= SlowlyReturnToNormal;
    }

    IEnumerator MoveTimeScale(float _target, float _time)
    {
        while (Mathf.Abs(Time.timeScale - _target) > minOffset)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, _target, _time);
            yield return new WaitForFixedUpdate();
        }

        Time.timeScale = _target;

        if (reachedTarget != null)
        {
            reachedTarget();
        }
    }

    public void StopSlowTime()
    {
        if (slowTimeActive)
        {
            reachedTarget -= SlowlyReturnToNormal;

            StopCoroutine(moveTimeScale);

            slowTimeActive = false;

            Time.timeScale = 1;
        }
    }

    public bool SlowTimeActive
    {
        get { return slowTimeActive; }
    }
}
