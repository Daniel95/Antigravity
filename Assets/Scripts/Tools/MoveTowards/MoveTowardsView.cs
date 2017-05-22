using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [SerializeField] private float speed = 10;
    [SerializeField] private float minReachedDistance = 0.5f;

    private Coroutine moveToCoroutine;

    public void StartMoveTowards(Vector2 destination) {
        StopMoving();
        moveToCoroutine = StartCoroutine(MoveTo(destination));
    }

    public void StartMoving(Vector2 destination, Signal onMoveTowardsCompletedEvent) {
        StartMoving(destination, () => onMoveTowardsCompletedEvent.Dispatch());
    }

    public void StartMoving(Vector2 destination, Action onMoveTowardsCompleted) {
        StopMoving();
        moveToCoroutine = StartCoroutine(MoveTo(destination, onMoveTowardsCompleted));
    }

    private IEnumerator MoveTo(Vector2 destination, Action onMoveTowardsCompleted = null) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = destination;

        if (onMoveTowardsCompleted != null) {
            onMoveTowardsCompleted();
        }
        moveToCoroutine = null;
    }

    public void StopMoving() {
        if (moveToCoroutine == null) { return; }
        StopCoroutine(moveToCoroutine);
        moveToCoroutine = null;
    }
}
