using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [SerializeField] private float speed = 10;
    [SerializeField] private float minReachedDistance = 0.5f;

    private Coroutine moveToPositionCoroutine;
    private Coroutine moveToTransformCoroutine;

    public void StartMoving(Vector2 destination, Signal onMoveTowardsCompletedEvent = null) {
        StartMoving(destination, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMoving(Vector2 destination, Action onMoveTowardsCompleted = null) {
        StopMoving();
        moveToPositionCoroutine = StartCoroutine(MoveToPosition(destination, onMoveTowardsCompleted));
    }

    public void StartMoving(Transform target, Signal onMoveTowardsCompletedEvent = null) {
        StartMoving(target, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMoving(Transform target, Action onMoveTowardsCompleted = null) {
        StopMoving();
        moveToTransformCoroutine = StartCoroutine(MoveToTransform(target, onMoveTowardsCompleted));
    }

    private IEnumerator MoveToPosition(Vector2 destination, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = destination;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToPositionCoroutine = null;
    }

    private IEnumerator MoveToTransform(Transform target, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, target.position) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = target.position;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToTransformCoroutine = null;
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
    }
}
