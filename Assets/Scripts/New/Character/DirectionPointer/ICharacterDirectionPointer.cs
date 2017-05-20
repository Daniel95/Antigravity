using UnityEngine;

public interface ICharacterDirectionPointer {

    void PointToCeiledVelocityDirection(Vector2 ceiledVelocityDirection);
    void PointToDirection(Vector2 direction);
}
