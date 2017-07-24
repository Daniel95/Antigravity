using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [SerializeField] private float speed = 3;
    [SerializeField] private float minReachedDistance = 0.1f;

    private Coroutine moveToPositionCoroutine;
    private Coroutine moveToTransformCoroutine;
    private Coroutine moveToDirectionCoroutine;

    public void StartMovingToDirection(Vector2 direction) {
        StopMoving();
        moveToDirectionCoroutine = StartCoroutine(MoveToDirection(direction));
    }

    public void StartMovingToTarget(Vector2 targetPosition, Signal onMoveTowardsCompletedEvent = null) {
        StartMovingToTarget(targetPosition, EasingType.EaseLinear, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMovingToTarget(Vector2 targetPosition, EasingType easingType = EasingType.EaseLinear, Signal onMoveTowardsCompletedEvent = null) {
        StartMovingToTarget(targetPosition, easingType, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMovingToTarget(Vector2 targetDestination, EasingType easingType = EasingType.EaseLinear, Action onMoveTowardsCompleted = null) {
        StopMoving();
        Vector2 startPosition = transform.position;
        moveToPositionCoroutine = StartCoroutine(EasedLerpPosition(startPosition, targetDestination, easingType, onMoveTowardsCompleted));
    }

    public void StartMovingToTarget(Transform target, Signal onMoveTowardsCompletedEvent = null) {
        StartMovingToTarget(target, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMovingToTarget(Transform target, Action onMoveTowardsCompleted = null) {
        StopMoving();
        moveToTransformCoroutine = StartCoroutine(MoveToTransform(target, onMoveTowardsCompleted));
    }

    private IEnumerator EasedLerpPosition(Vector2 start, Vector2 destination, EasingType easingType = EasingType.EaseLinear, Action onFinishMoveAlongPath = null) {
        float time = 0.0f;

        float fromToOnPathDistance = Vector3.Distance(start, destination);

        float duration = fromToOnPathDistance / speed;

        while (time < duration) {
            float progress = time / duration;
            float easedProgress = EasingHelper.Ease(easingType, progress);

            time += Time.deltaTime;

            transform.position = Vector2.Lerp(start, destination, easedProgress);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        if (onFinishMoveAlongPath != null) {
            onFinishMoveAlongPath();
        }
    }

    /*
    private IEnumerator MoveToPosition(Vector2 destination, EasingType easingType = EasingType.EaseLinear, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToPositionCoroutine = null;
    }
    */

    private IEnumerator MoveToTransform(Transform target, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, target.position) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target.position;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToTransformCoroutine = null;
    }

    private IEnumerator MoveToDirection(Vector2 direction) {
        while (true) {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + (direction * speed), speed * Time.deltaTime);
            yield return null;
        }
    }

    public void StopMoving() {
        if (moveToPositionCoroutine != null) {
            StopCoroutine(moveToPositionCoroutine);
            moveToPositionCoroutine = null;
        }
        if (moveToTransformCoroutine != null) {
            StopCoroutine(moveToTransformCoroutine);
            moveToTransformCoroutine = null;
        }
        if(moveToDirectionCoroutine != null) {
            StopCoroutine(moveToDirectionCoroutine);
            moveToDirectionCoroutine = null;
        }
    }
}
