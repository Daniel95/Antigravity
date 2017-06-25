using IoCPlus;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterVelocityView : View, ICharacterVelocity {

    public Vector2 Velocity { get { return rigidbodyComponent.velocity; } set { rigidbodyComponent.velocity = value; } }
    public Vector2 PreviousVelocity { get { return previousVelocity; } }
    public Vector2 MoveDirection { get { return moveDirection; } }
    public Vector2 StartDirection { get { return startDirection; } }
    public float OriginalSpeed { get { return originalSpeed; }  }
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
            rigidbodyComponent.velocity = this.moveDirection * currentSpeed;
        }
    }

    public void AddVelocity(Vector2 velocity) {
        rigidbodyComponent.velocity += velocity;
    }

    public void SwitchVelocity() {
        rigidbodyComponent.velocity *= -1;
    }

    public void SwitchMoveDirection() {
        moveDirection *= -1;
    }

    public Vector2 GetVelocityDirection() {
        return rigidbodyComponent.velocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        return GetCeilDirection(rigidbodyComponent.velocity);
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
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
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
            rigidbodyComponent.velocity = moveDirection * currentSpeed;
            previousVelocity = rigidbodyComponent.velocity;
            yield return new WaitForFixedUpdate();
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

