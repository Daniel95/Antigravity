using IoCPlus;
using System;
using UnityEngine;

public interface IMoveTowards {
    void StartMoving(Vector2 destination, Action moveTowardsCompleted = null);
    void StartMoving(Vector2 destination, Signal moveTowardsCompletedEvent = null);
    void StartMoving(Transform target, Action moveTowardsCompleted = null);
    void StartMoving(Transform target, Signal moveTowardsCompletedEvent = null);
    void StopMoving();
}
