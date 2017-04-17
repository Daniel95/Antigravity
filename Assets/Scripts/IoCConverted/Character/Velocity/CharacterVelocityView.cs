using IoCPlus;
using System.Collections;
using UnityEngine;

public class CharacterVelocityView : View, ICharacterVelocity {

    public Vector2 Velocity { get { return rigidbodyComponent.velocity; } set { rigidbodyComponent.velocity = value; } }
    public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
    public float OriginalSpeed { get { return originalSpeed; }  }
    public float CurrentSpeed { get { return currentSpeed; } }

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [SerializeField] private float originalSpeed = 3;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minSpeedOffsetValue = 0.05f;
    [SerializeField] private Vector2 moveDirection;

    private Rigidbody2D rigidbodyComponent;
    private Coroutine updateDirectionalMovement;
    private Coroutine returnSpeedToOriginal;

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
        if (returnSpeedToOriginal != null)
            StopCoroutine(returnSpeedToOriginal);

        returnSpeedToOriginal = StartCoroutine(ReturnSpeedToOriginal(returnSpeed));
    }

    public void AddVelocity(Vector2 velocity) {
        rigidbodyComponent.velocity += velocity;
    }

    public void SwitchVelocityDirection() {
        rigidbodyComponent.velocity *= -1;
    }

    public void SwitchDirection() {
        moveDirection *= -1;
    }

    public Vector2 GetVelocityDirection() {
        return rigidbodyComponent.velocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        Vector2 velocityNormalized = rigidbodyComponent.velocity.normalized;
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
    }

    public void SetSpeed(float newSpeed) {
        if (returnSpeedToOriginal != null) {
            StopCoroutine(returnSpeedToOriginal);
        }

        currentSpeed = newSpeed;
    }

    public bool GetMovingStandard() {
        return updateDirectionalMovement != null;
    }

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        currentSpeed = originalSpeed;
    }

    private void EnableDirectionalMovement() {
        DisableDirectionalMovement();
        updateDirectionalMovement = StartCoroutine(UpdateDirectionalMovement());
    }

    private void DisableDirectionalMovement() {
        if (updateDirectionalMovement != null)
        {
            StopCoroutine(updateDirectionalMovement);
            updateDirectionalMovement = null;
        }
    }

    private IEnumerator UpdateDirectionalMovement() {
        var fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            //add our own constant force
            rigidbodyComponent.velocity = moveDirection * currentSpeed;
            yield return fixedUpdate;
        }
    }

    private IEnumerator ReturnSpeedToOriginal(float returnSpeed) {
        var fixedUpdate = new WaitForFixedUpdate();
        while (Mathf.Abs(currentSpeed - originalSpeed) > minSpeedOffsetValue)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, returnSpeed * (CurrentSpeed / originalSpeed));
            yield return fixedUpdate;
        }

        currentSpeed = originalSpeed;
    }
}

