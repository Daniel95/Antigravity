using UnityEngine;
using System.Collections;
using System;

public class Frames : MonoBehaviour {

    public static bool CheckInterval(int _intervalLength)
    {
        if (Time.frameCount % _intervalLength == 0)
            return true;
        else
            return false;
    }

    public void ExecuteAfterDelay(int _framesToWait, Action _action) {
        StartCoroutine(DelayExecute(_framesToWait, _action));
    }

    IEnumerator DelayExecute(int _framesToWait, Action _action) {
        for (int i = 0; i < _framesToWait; i++) {
            yield return new WaitForFixedUpdate();
        }

        _action();
    }
}
