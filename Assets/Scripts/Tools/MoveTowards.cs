using UnityEngine;
using System;
using System.Collections;

public class MoveTowards : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float minReachedDistance = 0.5f;

    public Action ReachedDestination;

    private Coroutine _moveTo;

    public void StartMoving(Vector2 destination) {
        //if we are still busy moving towards something, cancel it
        if (_moveTo != null) {
            StopMoving();
        }

        _moveTo = StartCoroutine(MoveTo(destination));
    }

    //moves itself to the destination with a const force, activates reachedDestination delegate when the distance is small enough
    IEnumerator MoveTo(Vector2 destination) {

        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }

        _moveTo = null;

        transform.position = destination;

        if (ReachedDestination != null)
            ReachedDestination();
    }

    public void StopMoving() {
        StopCoroutine(_moveTo);
        _moveTo = null;
    }

    void OnDestroy() {
        ReachedDestination = null;
    }
}
