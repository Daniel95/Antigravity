using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollowerSmooth : MonoBehaviour, ITriggerable {

    public bool Triggered { get; set; }

    [SerializeField]
    private bool activateSelf = true;

    [SerializeField]
    private float minDistance = 0.3f;

    [SerializeField]
    private float mass = 75;

    [SerializeField]
    private float speed = 0.1f;

    private WayPoints _wayPoints;

    private Coroutine moveToPositionCoroutine;

    private Vector2 _currentVelocity;

    private void Start()
    {
        _wayPoints = GetComponent<WayPoints>();

        if (activateSelf)
            TriggerActivate();
    }

    public void StartFollowingWaypoint()
    {
        if (moveToPositionCoroutine != null)
            TriggerStop();

        moveToPositionCoroutine = StartCoroutine(MoveToPos(_wayPoints.GetCurrentPoint()));
    }

    IEnumerator MoveToPos(Vector2 _pos)
    {
        var fixedUpdate = new WaitForFixedUpdate();

        while (Mathf.Abs(transform.position.x - _pos.x) > minDistance || Mathf.Abs(transform.position.y - _pos.y) > minDistance)
        {
            //our direction to the target
            Vector2 dir = (_pos - (Vector2)transform.position).normalized;

            //our velocity is dir * speed, minus currentVelocity so we dont speed up
            Vector2 targetVelocity = dir * speed - _currentVelocity;

            //the bigger our massa, the slower we change our velocity
            _currentVelocity += targetVelocity / mass;

            transform.position += (Vector3) _currentVelocity;

            yield return fixedUpdate;
        }

        yield return fixedUpdate;

        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        moveToPositionCoroutine = StartCoroutine(MoveToPos(_wayPoints.GetNextPoint()));
    }

    public void TriggerActivate()
    {
        StartFollowingWaypoint();
    }

    public void TriggerStop() {
        StopCoroutine(moveToPositionCoroutine);
        moveToPositionCoroutine = null;
    }
}
