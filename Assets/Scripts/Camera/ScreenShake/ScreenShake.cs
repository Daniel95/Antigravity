using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    [SerializeField] private List<ShakeNode> shakeNodes;

    private Coroutine moveShakeStrengthCoroutine;
    private Coroutine shakeCoroutine;

    private float shakeStrength;

    public void ShakeInOut(ShakeType shakeType) {
        ShakeNode shakeNode = GetShakeNode(shakeType);

        ShakeIn(shakeNode.ShakeInData, () => ShakeOut(shakeNode.ShakeOutData));
    }

    public void ShakeIn(ShakeType shakeType, Action onShakeInCompleted = null) {
        StopShake();

        ShakeNode shakeNode = GetShakeNode(shakeType);

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeNode.ShakeInData, onShakeInCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeIn(ShakeData shakeInData, Action onShakeInCompleted = null) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeInData, onShakeInCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }


    public void ShakeOut(ShakeType shakeType, Action onShakeOutCompleted = null) {
        StopShake();

        ShakeNode shakeNode = GetShakeNode(shakeType);

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeNode.ShakeOutData, onShakeOutCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    public void ShakeOut(ShakeData shakeOutData, Action onShakeOutCompleted = null) {
        StopShake();

        moveShakeStrengthCoroutine = StartCoroutine(MoveShakeStrength(shakeOutData, onShakeOutCompleted));
        if (shakeCoroutine == null) {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake() {
        Vector2 startPosition = transform.position;

        while (shakeStrength > 0) {
            Vector2 randomizedOffset = MathHelper.GetRandomizedVector2(-shakeStrength, shakeStrength);
            transform.position = startPosition + randomizedOffset;
            yield return new WaitForEndOfFrame();
        }

        transform.position = startPosition;

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

            float strengthProgess = targetStrength - startStrength;
            float easedStrengthProgess = strengthProgess * easedAbsoluteValue;

            shakeStrength = startStrength + easedStrengthProgess;

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

    private ShakeNode GetShakeNode(ShakeType shakeType) {
        return shakeNodes.Find(x => x.ShakeType == shakeType);
    }

}