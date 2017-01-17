using UnityEngine;
using System.Collections;
using System;

public class Frames : MonoBehaviour {

    public static bool CheckInterval(int intervalLength)
    {
        return Time.frameCount % intervalLength == 0;
    }

    public void ExecuteAfterDelay(int framesToWait, Action action) {
        StartCoroutine(DelayExecute(framesToWait, action));
    }

    IEnumerator DelayExecute(int framesToWait, Action action) {
        for (int i = 0; i < framesToWait; i++) {
            yield return new WaitForFixedUpdate();
        }

        action();
    }
}
