using UnityEngine;

public interface ICharacterVelocity {

    Vector2 Velocity { get; }
    bool IsMovingStandard { get; }
    Vector2 PreviousVelocity { get; }
    Vector2 MoveDirection { get; }
    Vector2 StartDirection { get; }
    float OriginalSpeed { get; }
    float CurrentSpeed { get; }

    Vector2 GetVelocityDirection();
    Vector2 GetCeilVelocityDirection();
    Vector2 GetPreviousVelocityDirection();
    Vector2 GetCeilPreviousVelocityDirection();

    void EnableDirectionalMovement();
    void DisableDirectionalMovement();
    void StartReturnSpeedToOriginal(float returnSpeed);
    void SetMoveDirection(Vector2 moveDirection);
    void AddVelocity(Vector2 velocity);
    void SetVelocity(Vector2 velocity);
    void SetSpeed(float newSpeed);
    void SwitchVelocity();
    void SwitchMoveDirection();

}
