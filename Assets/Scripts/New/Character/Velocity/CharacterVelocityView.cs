using IoCPlus;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterVelocityView : View, ICharacterVelocity {

    public Vector2 Velocity { get { return rigidbodyComponent.velocity; } set { rigidbodyComponent.velocity = value; } }
    public Vector2 Direction { get { return direction; } }
    public float OriginalSpeed { get { return originalSpeed; }  }
    public float CurrentSpeed { get { return currentSpeed; } }

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [SerializeField] private float originalSpeed = 3;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minSpeedOffsetValue = 0.05f;

    private Vector2 direction = new Vector2(1, -1);
    private Rigidbody2D rigidbodyComponent;
    private Coroutine updateDirectionalMovementCoroutine;
    private Coroutine returnSpeedToOriginalCoroutine;

    public override void Initialize() {
        controlVelocityRef.Set(this);
    }

    public void EnableDirectionalMovement(bool enable) {
        if(enable) {
            EnableDirectionalMovement();
        } else {
            DisableDirectionalMovement();
        }
    }

    public void StartReturnSpeedToOriginal(float returnSpeed) {
        if (returnSpeedToOriginalCoroutine != null) {
            StopCoroutine(returnSpeedToOriginalCoroutine);
            returnSpeedToOriginalCoroutine = null;
        }

        returnSpeedToOriginalCoroutine = StartCoroutine(ReturnSpeedToOriginal(returnSpeed));
    }

    public void SetMoveDirection(Vector2 moveDirection) {
        direction = moveDirection;
        rigidbodyComponent.velocity = direction * currentSpeed;
    }

    public void AddVelocity(Vector2 velocity) {
        rigidbodyComponent.velocity += velocity;
    }

    public void SwitchVelocityDirection() {
        rigidbodyComponent.velocity *= -1;
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    public Vector2 GetVelocityDirection() {
        return rigidbodyComponent.velocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        Vector2 velocityNormalized = rigidbodyComponent.velocity.normalized;
        Debug.Log(new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y)));
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
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

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        currentSpeed = originalSpeed;
    }

    private void EnableDirectionalMovement() {
        DisableDirectionalMovement();
        updateDirectionalMovementCoroutine = StartCoroutine(UpdateDirectionalMovement());
    }

    private void DisableDirectionalMovement() {
        if (updateDirectionalMovementCoroutine != null) {
            StopCoroutine(updateDirectionalMovementCoroutine);
            updateDirectionalMovementCoroutine = null;
        }
    }

    private IEnumerator UpdateDirectionalMovement() {
        while (true) {
            rigidbodyComponent.velocity = direction * currentSpeed;
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
}

