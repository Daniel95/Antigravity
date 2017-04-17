using UnityEngine;

public interface ICharacterVelocity {

    Vector2 Velocity { get; set; }
    Vector2 MoveDirection { get; set; }
    float OriginalSpeed { get; }
    float CurrentSpeed { get; }

    Vector2 GetVelocityDirection();
    Vector2 GetCeilVelocityDirection();
    bool GetMovingStandard();

    void EnableDirectionalMovement(bool enable);
    void StartReturnSpeedToOriginal(float returnSpeed);
    void AddVelocity(Vector2 velocity);
    void SetSpeed(float newSpeed);
    void SwitchVelocityDirection();
    void SwitchDirection();
}
