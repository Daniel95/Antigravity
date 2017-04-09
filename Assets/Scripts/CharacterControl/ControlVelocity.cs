using UnityEngine;
using System;
using System.Collections;

public class ControlVelocity : MonoBehaviour {

    [SerializeField]
    private float originalSpeed = 3;

    [SerializeField]
    private float _currentSpeed;

    [SerializeField]
    private float minSpeedOffsetValue = 0.05f;

    [SerializeField]
    private Vector2 direction;

    private Rigidbody2D _rb;

    private Coroutine _updateDirectionalMovement;

    private Coroutine _returnSpeedToOriginal;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = originalSpeed;
    }

    /// <summary>
    /// Starts moving the rigidbody towards the controlled direction. (GetDirection)
    /// </summary>
    public void StartDirectionalMovement()
    {
        StopDirectionalMovement();
        _updateDirectionalMovement = StartCoroutine(UpdateDirectionalMovement());
    }

    public void StopDirectionalMovement()
    {
        if (_updateDirectionalMovement != null)
        {
            StopCoroutine(_updateDirectionalMovement);
            _updateDirectionalMovement = null;
        }
    }

    private IEnumerator UpdateDirectionalMovement()
    {
        var fixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            //add our own constant force
            _rb.velocity = direction * _currentSpeed;
            yield return fixedUpdate;
        }
    }

    /// <summary>
    /// Returns the speed to normal.
    /// </summary>
    /// <param name="returnSpeed"></param>
    public void StartReturnSpeedToOriginal(float returnSpeed)
    {
        if (_returnSpeedToOriginal != null)
            StopCoroutine(_returnSpeedToOriginal);

        _returnSpeedToOriginal = StartCoroutine(ReturnSpeedToOriginal(returnSpeed));
    }

    private IEnumerator ReturnSpeedToOriginal(float returnSpeed)
    {
        var fixedUpdate = new WaitForFixedUpdate();
        while (Mathf.Abs(_currentSpeed - originalSpeed) > minSpeedOffsetValue)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, originalSpeed, returnSpeed * (CurrentSpeed / originalSpeed));
            yield return fixedUpdate;
        }

        _currentSpeed = originalSpeed;
    }

    public void AddVelocity(Vector2 velocity) {
        _rb.velocity += velocity;
    }

    public void SetVelocity(Vector2 velocity) {
        _rb.velocity = velocity;
    }

    //switched to direction of the velocity
    public void SwitchVelocityDirection()
    {
        SetVelocity(GetVelocity * -1);
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    //set the direction without adjusting anything
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    /// <summary>
    /// get our own controlled direction
    /// </summary>
    /// <returns></returns>
    public Vector2 GetDirection() {
        return direction;
    }

    /// <summary>
    /// returns the realtime direction of the velocity.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetVelocityDirection() {
        return _rb.velocity.normalized;
    }

    /// <summary>
    /// get the ceiled (inverted ceil on negative) version of our velocity direction.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetCeilVelocityDirection() {
        Vector2 velocityNormalized = _rb.velocity.normalized;
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
    }

    /// <summary>
    /// Stop any speed modifiers (coroutines), and set the speed to the new value, or the max value if the new value is too large.
    /// </summary>
    /// <param name="newSpeed"></param>
    public void SetSpeed(float newSpeed)
    {
        if(_returnSpeedToOriginal != null)
        {
            StopCoroutine(_returnSpeedToOriginal);
        }

        _currentSpeed = newSpeed;
    }

    public Vector2 GetVelocity {
        get { return _rb.velocity; }
    }

    public float OriginalSpeed
    {
        get { return originalSpeed; }
    }

    public float CurrentSpeed {
        get { return _currentSpeed; }
    }

    public bool CheckMovingStandard() {
        return _updateDirectionalMovement != null;
    }
}

