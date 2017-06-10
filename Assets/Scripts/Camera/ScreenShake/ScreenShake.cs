using System;
using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    [Serializable]
    public struct ShakeData {
        public float duration;
        public float randomDurationOffset;
        public float targetStrength;
        public float randomTargetStrengthOffset;
    }

    [SerializeField] private ShakeData shakeInData;
    [SerializeField] private ShakeData shakeOutData;

    private Coroutine moveShakeStrengthCoroutine;
    private Coroutine shakeCoroutine;

    private float shakeStrength;

    public void ShakeInOut() {
        ShakeIn(ShakeOut);
    }

    public void ShakeInOut(ShakeData inData, ShakeData outData) {
        ShakeIn(inData, () => ShakeOut(outData));
    }

    public void ShakeIn() {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeInData));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeIn(Action onShakeInCompleted) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeInData, onShakeInCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeIn(ShakeData data, Action onShakeInCompleted = null) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(data, onShakeInCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeOut() {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeOutData));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeOut(Action onShakeOutCompleted = null) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeOutData, onShakeOutCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeOut(ShakeData data, Action onShakeOutCompleted = null) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(data, onShakeOutCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake() {
        yield return new WaitForEndOfFrame();
        while (shakeStrength > 0) {
            Vector3 randomOffset = Vector3.zero;
            randomOffset.x = UnityEngine.Random.Range(-shakeStrength, shakeStrength);
            randomOffset.y = UnityEngine.Random.Range(-shakeStrength, shakeStrength);
            randomOffset.z = UnityEngine.Random.Range(-shakeStrength, shakeStrength);

            transform.localPosition = new Vector3(randomOffset.x, randomOffset.y, randomOffset.z);
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = Vector3.zero;

        shakeCoroutine = null;
    }

    private IEnumerator MoveShakeStrength(ShakeData shakeData, Action onShakeCompleted = null) {
        float durationOffset = UnityEngine.Random.Range(-shakeData.randomDurationOffset, shakeData.randomDurationOffset);
        float targetStrengthOffset = UnityEngine.Random.Range(-shakeData.randomTargetStrengthOffset, shakeData.randomTargetStrengthOffset);

        float duration = shakeData.duration + durationOffset;
        float targetStrength = shakeData.targetStrength + targetStrengthOffset;
        float startStrength = shakeStrength;

        float time = 0;

        while (time < duration) {
            float absoluteValue = time / duration;
            float easedAbsoluteValue = EasingHelper.EaseInSine(absoluteValue);
            shakeStrength = startStrength + (targetStrength - startStrength) * easedAbsoluteValue;

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        shakeStrength = targetStrength;

        if (onShakeCompleted != null) {
            onShakeCompleted();
        }
    }

    private void StopShake() {
        if (moveShakeStrengthCoroutine != null) {
            StopCoroutine(moveShakeStrengthCoroutine);
            moveShakeStrengthCoroutine = null;
        }
    }
}