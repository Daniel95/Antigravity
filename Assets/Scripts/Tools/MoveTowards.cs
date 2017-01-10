using UnityEngine;
using System;
using System.Collections;

public class MoveTowards : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float minReachedDistance = 0.5f;

    public Action reachedDestination;

    private Coroutine moveTo;

    public void StartMoving(Vector2 _destination) {
        //if we are still busy moving towards something, cancel it
        if (moveTo != null) {
            StopMoving();
        }

        moveTo = StartCoroutine(MoveTo(_destination));
    }

    //moves itself to the destination with a const force, activates reachedDestination delegate when the distance is small enough
    IEnumerator MoveTo(Vector2 _destination) {

        while (Vector2.Distance(transform.position, _destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, _destination, speed);
            yield return new WaitForFixedUpdate();
        }

        moveTo = null;

        transform.position = _destination;

        if (reachedDestination != null)
            reachedDestination();
    }

    public void StopMoving() {
        StopCoroutine(moveTo);
        moveTo = null;
    }

    void OnDestroy() {
        reachedDestination = null;
    }
}
