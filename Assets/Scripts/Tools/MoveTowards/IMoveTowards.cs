using IoCPlus;
using System;
using UnityEngine;

public interface IMoveTowards {

    void StartMovingToDirection(Vector2 direction);
    void StartMovingToTarget(Vector2 targetDestination, Signal moveTowardsCompletedEvent = null);
    void StartMovingToTarget(Vector2 targetDestination, EasingType easingType = EasingType.EaseOutSine, Signal moveTowardsCompletedEvent = null);
    void StartMovingToTarget(Vector2 targetDestination, EasingType easingType = EasingType.EaseOutSine, Action moveTowardsCompleted = null);
    void StartMovingToTarget(Transform target, Signal moveTowardsCompletedEvent = null);
    void StartMovingToTarget(Transform target, Action moveTowardsCompleted = null);
    void StopMoving();

}
