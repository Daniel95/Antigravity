using IoCPlus;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [Inject] private ReachedDestinationEvent reachedDestinationEvent;

    [SerializeField] private float speed = 10;
    [SerializeField] private float minReachedDistance = 0.5f;

    private Coroutine moveTo;

    public void StartMoving(Vector2 destination) {
        if (moveTo != null) {
            StopMoving();
        }

        moveTo = StartCoroutine(MoveTo(destination));
    }

    private IEnumerator MoveTo(Vector2 destination) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = destination;
        reachedDestinationEvent.Dispatch(gameObject);
        moveTo = null;
    }

    public void StopMoving() {
        StopCoroutine(moveTo);
        moveTo = null;
    }
}
