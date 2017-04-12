using UnityEngine;
using System.Collections;

public interface ICharacterVelocity {

    Vector2 Velocity { get; set; }
    Vector2 Direction { get; set; }
    float OriginalSpeed { get; }
    float CurrentSpeed { get; }

    Vector2 VelocityDirection();
    Vector2 CeilVelocityDirection();
    bool MovingStandard();

    void EnableDirectionalMovement(bool enable);
    void StartReturnSpeedToOriginal(float returnSpeed);
    void AddVelocity(Vector2 velocity);
    void SetSpeed(float newSpeed);
    void SwitchVelocityDirection();
    void SwitchDirection();
}
