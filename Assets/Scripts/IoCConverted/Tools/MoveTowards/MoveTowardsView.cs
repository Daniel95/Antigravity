using IoCPlus;
using System.Collections;
using UnityEngine;

public class MoveTowardsView : View, IMoveTowards {

    [Inject] private Ref<IMoveTowards> moveTowardsRef;

    [Inject] private ReachedMoveTowardsDestinationEvent reachedDestinationEvent;

    [SerializeField] private float speed = 10;
    [SerializeField] private float minReachedDistance = 0.5f;

    private Coroutine moveToCoroutine;

    public override void Initialize() {
        moveTowardsRef.Set(this);
        Debug.Log(moveTowardsRef.Get());
    }

    public void StartMoving(Vector2 destination) {

        Debug.Log("start moving");
        if (moveToCoroutine != null) {
            StopMoving();
        }

        moveToCoroutine = StartCoroutine(MoveTo(destination));
    }

    private IEnumerator MoveTo(Vector2 destination) {
        while (Vector2.Distance(transform.position, destination) > minReachedDistance) {
            Debug.Log("moving");
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            yield return new WaitForFixedUpdate();
        }

        gameObject.transform.position = destination;
        reachedDestinationEvent.Dispatch(gameObject);
        moveToCoroutine = null;
    }

    public void StopMoving() {
        StopCoroutine(moveToCoroutine);
        moveToCoroutine = null;
    }
}
