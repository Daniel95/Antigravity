using UnityEngine;
using System;
using System.Collections;
using IoCPlus;

public class CharacterVelocityView : View, ICharacterVelocity {

    public Vector2 Velocity { get { return _rb.velocity; } set { _rb.velocity = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public float OriginalSpeed { get { return originalSpeed; }  }
    public float CurrentSpeed { get { return currentSpeed; } }

    [Inject] private Ref<ICharacterVelocity> controlVelocityRef;

    [SerializeField] private float originalSpeed = 3;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minSpeedOffsetValue = 0.05f;
    [SerializeField] private Vector2 direction;

    private Rigidbody2D _rb;
    private Coroutine _updateDirectionalMovement;
    private Coroutine _returnSpeedToOriginal;

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
        if (_returnSpeedToOriginal != null)
            StopCoroutine(_returnSpeedToOriginal);

        _returnSpeedToOriginal = StartCoroutine(ReturnSpeedToOriginal(returnSpeed));
    }

    public void AddVelocity(Vector2 velocity) {
        _rb.velocity += velocity;
    }

    public void SwitchVelocityDirection() {
        _rb.velocity *= -1;
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    public Vector2 VelocityDirection() {
        return _rb.velocity.normalized;
    }

    public Vector2 CeilVelocityDirection() {
        Vector2 velocityNormalized = _rb.velocity.normalized;
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
    }

    public void SetSpeed(float newSpeed) {
        if (_returnSpeedToOriginal != null) {
            StopCoroutine(_returnSpeedToOriginal);
        }

        currentSpeed = newSpeed;
    }

    public bool MovingStandard() {
        return _updateDirectionalMovement != null;
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        currentSpeed = originalSpeed;
    }

    private void EnableDirectionalMovement() {
        DisableDirectionalMovement();
        _updateDirectionalMovement = StartCoroutine(UpdateDirectionalMovement());
    }

    private void DisableDirectionalMovement() {
        if (_updateDirectionalMovement != null)
        {
            StopCoroutine(_updateDirectionalMovement);
            _updateDirectionalMovement = null;
        }
    }

    private IEnumerator UpdateDirectionalMovement() {
        var fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            //add our own constant force
            _rb.velocity = direction * currentSpeed;
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

