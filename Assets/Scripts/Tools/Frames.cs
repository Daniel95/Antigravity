using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Frames : MonoBehaviour {

    //private List<Coroutine> runningCoroutines = new List<Coroutine>();

    public static bool CheckInterval(int _intervalLength)
    {
        return Time.frameCount % _intervalLength == 0;
    }

    public Coroutine ExecuteAfterDelay(int _framesToWait, Action _action) {
        return StartCoroutine(DelayExecute(_framesToWait, _action));
    }

    private IEnumerator DelayExecute(int _framesToWait, Action _action) {
        var fixedUpdate = new WaitForFixedUpdate();

        for (int i = 0; i < _framesToWait; i++) {
            yield return fixedUpdate;
        }

        _action();
    }

    public void StopDelayExecute(Coroutine coroutine) {
        StopCoroutine(coroutine);
    }
}
