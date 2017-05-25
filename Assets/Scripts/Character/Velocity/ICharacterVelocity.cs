﻿using UnityEngine;

public interface ICharacterVelocity {

    Vector2 Velocity { get; set; }
    Vector2 PreviousVelocity { get; }
    Vector2 Direction { get; }
    float OriginalSpeed { get; }
    float CurrentSpeed { get; }

    Vector2 GetVelocityDirection();
    Vector2 GetCeilVelocityDirection();
    Vector2 GetPreviousVelocityDirection();
    Vector2 GetCeilPreviousVelocityDirection();
    bool GetMovingStandard();

    void EnableDirectionalMovement(bool enable);
    void StartReturnSpeedToOriginal(float returnSpeed);
    void SetMoveDirection(Vector2 moveDirection);
    void AddVelocity(Vector2 velocity);
    void SetSpeed(float newSpeed);
    void SwitchVelocityDirection();
    void SwitchDirection();
}
