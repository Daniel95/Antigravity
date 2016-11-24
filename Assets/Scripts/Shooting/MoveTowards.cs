using UnityEngine;
using System;
using System.Collections;

public class MoveTowards : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float minReachedDistance = 0.5f;

    public Action reachedDestination;

    public void StartMoving(Vector2 _destination) {
        StartCoroutine(MoveTo(_destination));
    }

    //moves itself to the destination with a const force, activates reachedDestination delegate when the distance is small enough
    IEnumerator MoveTo(Vector2 _destination) {
        while (Vector2.Distance(transform.position, _destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, _destination, speed);
            yield return new WaitForFixedUpdate();
        }

        transform.position = _destination;

        if (reachedDestination != null)
            reachedDestination();
    }

    void OnDestroy() {
        reachedDestination = null;
    }
}
