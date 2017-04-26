using UnityEngine;

public interface IMoveTowards {
    void StartMoving(Vector2 destination);
    void StopMoving();
}
