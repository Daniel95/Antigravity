﻿using IoCPlus;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterVelocityView : View, ICharacterVelocity {

    public Vector2 Velocity {
        get {
            float doubleDecimalX = (float)Math.Round(rigidbodyComponent.velocity.x, 2);
            float doubleDecimalY = (float)Math.Round(rigidbodyComponent.velocity.y, 2);
            return new Vector2(doubleDecimalX, doubleDecimalY);
        }
    }
    public bool IsMovingStandard {
        get {
            return updateDirectionalMovementCoroutine != null;
        }
    }
    public Vector2 PreviousVelocity { get { return previousVelocity; } }
    public Vector2 MoveDirection { get { return moveDirection; } }
    public Vector2 StartDirection { get { return startDirection; } }
    public float OriginalSpeed { get { return originalSpeed; } }
    public float CurrentSpeed { get { return currentSpeed; } }

    [SerializeField] private Vector2 startDirection = new Vector2(1, -1);
    [SerializeField] private float originalSpeed = 3.2f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minSpeedOffsetValue = 0.05f;

    private Vector2 previousVelocity = new Vector2();
    private Vector2 moveDirection = new Vector2();
    private Rigidbody2D rigidbodyComponent;
    private Coroutine updateDirectionalMovementCoroutine;
    private Coroutine returnSpeedToOriginalCoroutine;

    public void StartReturnSpeedToOriginal(float returnSpeed) {
        if (returnSpeedToOriginalCoroutine != null) {
            StopCoroutine(returnSpeedToOriginalCoroutine);
            returnSpeedToOriginalCoroutine = null;
        }

        returnSpeedToOriginalCoroutine = StartCoroutine(ReturnSpeedToOriginal(returnSpeed));
    }

    public void SetMoveDirection(Vector2 moveDirection) {
        this.moveDirection = moveDirection;
        if (updateDirectionalMovementCoroutine != null) {
            previousVelocity = Velocity;
            rigidbodyComponent.velocity = this.moveDirection * currentSpeed;
        }
    }

    public void AddVelocity(Vector2 velocity) {
        previousVelocity = velocity;
        rigidbodyComponent.velocity += velocity;
    }

    public void SetVelocity(Vector2 velocity) {
        previousVelocity = velocity;
        rigidbodyComponent.velocity = velocity;
    }

    public void SwitchVelocity() {
        previousVelocity = Velocity;
        rigidbodyComponent.velocity *= -1;
    }

    public void SwitchMoveDirection() {
        moveDirection *= -1;
    }

    public Vector2 GetVelocityDirection() {
        return Velocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        return GetCeilDirection(Velocity);
    }

    public Vector2 GetCeilPreviousVelocityDirection() {
        return GetCeilDirection(previousVelocity);
    }

    public Vector2 GetPreviousVelocityDirection() {
        return previousVelocity.normalized;
    }

    public void SetSpeed(float newSpeed) {
        if (returnSpeedToOriginalCoroutine != null) {
            StopCoroutine(returnSpeedToOriginalCoroutine);
            returnSpeedToOriginalCoroutine = null;
        }

        currentSpeed = newSpeed;
    }

    public bool GetMovingStandard() {
        return updateDirectionalMovementCoroutine != null;
    }

    public Vector2 GetCeilDirection(Vector2 velocity) {
        Vector2 velocityNormalized = velocity.normalized;
        Vector2 ceilDirection = new Vector2(RoundingHelper.InvertOnNegativeCeil(velocityNormalized.x), RoundingHelper.InvertOnNegativeCeil(velocityNormalized.y));
        return ceilDirection;
    }

    public void EnableDirectionalMovement() {
        DisableDirectionalMovement();
        updateDirectionalMovementCoroutine = StartCoroutine(UpdateDirectionalMovement());
    }

    public void DisableDirectionalMovement() {
        if (updateDirectionalMovementCoroutine != null) {
            StopCoroutine(updateDirectionalMovementCoroutine);
            updateDirectionalMovementCoroutine = null;
        }
    }

    private IEnumerator UpdateDirectionalMovement() {
        while (true) {
            previousVelocity = Velocity;
            //Debug.Log("________ " + FrameHelper.FrameCount);
            //DebugHelper.LogPreciseVector(rigidbodyComponent.velocity, "Velocity");
            //DebugHelper.LogPreciseVector(moveDirection, "moveDirection");
            rigidbodyComponent.velocity = moveDirection * currentSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ReturnSpeedToOriginal(float returnSpeed) {
        while (Mathf.Abs(currentSpeed - originalSpeed) > minSpeedOffsetValue) {
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, returnSpeed * (currentSpeed / originalSpeed));
            yield return null;
        }

        currentSpeed = originalSpeed;
    }

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        currentSpeed = originalSpeed;
    }

}