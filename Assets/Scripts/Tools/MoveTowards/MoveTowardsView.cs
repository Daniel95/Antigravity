using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [SerializeField] private float speed = 10;
    [SerializeField] private float minReachedDistance = 0.5f;

    private Coroutine moveToPositionCoroutine;
    private Coroutine moveToTransformCoroutine;
    private Coroutine moveToDirectionCoroutine;

    public void StartMovingToDirection(Vector2 direction) {
        StopMoving();
        moveToDirectionCoroutine = StartCoroutine(MoveToDirection(direction));
    }

    public void StartMovingToTarget(Vector2 targetDestination, Signal onMoveTowardsCompletedEvent = null) {
        StartMovingToTarget(targetDestination, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMovingToTarget(Vector2 targetDestination, Action onMoveTowardsCompleted = null) {
        StopMoving();
        moveToPositionCoroutine = StartCoroutine(MoveToPosition(targetDestination, onMoveTowardsCompleted));
    }

    public void StartMovingToTarget(Transform target, Signal onMoveTowardsCompletedEvent = null) {
        StartMovingToTarget(target, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMovingToTarget(Transform target, Action onMoveTowardsCompleted = null) {
        StopMoving();
        moveToTransformCoroutine = StartCoroutine(MoveToTransform(target, onMoveTowardsCompleted));
    }

    private IEnumerator MoveToPosition(Vector2 destination, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        gameObject.transform.position = destination;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToPositionCoroutine = null;
    }

    private IEnumerator MoveToTransform(Transform target, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, target.position) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        gameObject.transform.position = target.position;

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
