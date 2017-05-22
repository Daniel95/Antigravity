using IoCPlus;
using System;
using UnityEngine;

public interface IMoveTowards {
    void StartMoveTowards(Vector2 destination);
    void StartMoving(Vector2 destination, Signal moveTowardsCompletedEvent = null);
    void StopMoving();
}
