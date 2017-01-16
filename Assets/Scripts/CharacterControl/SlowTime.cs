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

    private Coroutine _moveTimeScale;

    private bool _slowTimeActive;

    private Action _reachedTarget;


    /// <summary>
    /// Starts slowing the time, after timescale reaches its minimum it returns to normal over time.
    /// </summary>
    public void StartSlowTime()
    {
        if (_moveTimeScale != null)
        {
            StopCoroutine(_moveTimeScale);
        }

        _slowTimeActive = true;

        _reachedTarget += SlowlyReturnToNormal;

        //slow down the time
        _moveTimeScale = StartCoroutine(MoveTimeScale(slowTimeScale, slowDownTime));
    }

    private void SlowlyReturnToNormal()
    {
        _moveTimeScale = StartCoroutine(MoveTimeScale(1, returnSpeed));
        _reachedTarget -= SlowlyReturnToNormal;
    }

    IEnumerator MoveTimeScale(float _target, float _time)
    {
        while (Mathf.Abs(Time.timeScale - _target) > minOffset)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, _target, _time);
            yield return new WaitForFixedUpdate();
        }

        Time.timeScale = _target;

        if (_reachedTarget != null)
        {
            _reachedTarget();
        }
    }

    /// <summary>
    /// Reset the time to normal.
    /// </summary>
    public void StopSlowTime()
    {
        if (_slowTimeActive)
        {
            _reachedTarget -= SlowlyReturnToNormal;

            StopCoroutine(_moveTimeScale);

            _slowTimeActive = false;

            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Check if SlowTime is active
    /// </summary>
    public bool SlowTimeActive
    {
        get { return _slowTimeActive; }
    }
}
