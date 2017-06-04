using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Frames : MonoBehaviour {

    public static bool CheckInterval(int intervalLength) {
        return Time.frameCount % intervalLength == 0;
    }

    public Coroutine ExecuteAfterDelay(int framesToWait, Action _action) {
        return StartCoroutine(DelayExecute(framesToWait, _action));
    }

    private IEnumerator DelayExecute(int framesToWait, Action _action) {
        var fixedUpdate = new WaitForFixedUpdate();

        for (int i = 0; i < framesToWait; i++) {
            yield return fixedUpdate;
        }

        _action();
    }

    public void StopDelayExecute(Coroutine coroutine) {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
