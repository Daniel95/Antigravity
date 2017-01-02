using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollowerSmooth : MonoBehaviour, ITriggerable {

    public bool triggered { get; set; }

    [SerializeField]
    private bool activateSelf = true;

    [SerializeField]
    private float minDistance = 0.3f;

    [SerializeField]
    private float mass = 75;

    [SerializeField]
    private float speed = 0.1f;

    private WayPoints wayPoints;

    private Coroutine moveToPos;

    private Vector2 currentVelocity;

    private void Start()
    {
        wayPoints = GetComponent<WayPoints>();

        if (activateSelf)
            TriggerActivate();
    }

    public void StartFollowingWaypoint()
    {
        if (moveToPos != null)
            TriggerStop();

        moveToPos = StartCoroutine(MoveToPos(wayPoints.GetCurrentPoint()));
    }

    IEnumerator MoveToPos(Vector2 _pos)
    {
        while (Mathf.Abs(transform.position.x - _pos.x) > minDistance || Mathf.Abs(transform.position.y - _pos.y) > minDistance)
        {
            //our direction to the target
            Vector2 dir = (_pos - (Vector2)transform.position).normalized;

            //our velocity is dir * speed, minus currentVelocity so we dont speed up
            Vector2 targetVelocity = (dir * speed) - currentVelocity;

            //the bigger our massa, the slower we change our velocity
            currentVelocity += targetVelocity / mass;

            transform.position += (Vector3)currentVelocity;

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForFixedUpdate();

        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        moveToPos = StartCoroutine(MoveToPos(wayPoints.GetNextPoint()));
    }

    public void TriggerActivate()
    {
        StartFollowingWaypoint();
    }

    public void TriggerStop()
    {
        StopCoroutine(moveToPos);
    }
}
