using UnityEngine;
using System;
using System.Collections;

public class ControlVelocity : MonoBehaviour {

    [SerializeField]
    private float originalSpeed = 3;

    private float currentSpeed;

    private float speedMultiplier = 1;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float minSpeedOffsetValue = 0.05f;

    [SerializeField]
    private Vector2 direction;

    private float originalTargetSpeed;

    private Rigidbody2D rb;

    private  Coroutine updateDirectionalMovement;

    private Coroutine returnSpeedToOriginal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = originalSpeed;
    }

    public void StartDirectionalMovement()
    {
        StopDirectionalMovement();
        updateDirectionalMovement = StartCoroutine(UpdateDirectionalMovement());
    }

    public void StopDirectionalMovement()
    {
        if (updateDirectionalMovement != null)
        {
            StopCoroutine(updateDirectionalMovement);
            updateDirectionalMovement = null;
        }
    }

    IEnumerator UpdateDirectionalMovement()
    {
        while (true)
        {
            //add our own constant force
            rb.velocity = direction * (currentSpeed * speedMultiplier);
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartReturnSpeedToOriginal(float _returnSpeed)
    {
        if (returnSpeedToOriginal != null)
            StopCoroutine(returnSpeedToOriginal);

        returnSpeedToOriginal = StartCoroutine(ReturnSpeedToOriginal(_returnSpeed));
    }

    IEnumerator ReturnSpeedToOriginal(float _returnSpeed)
    {
        while (Mathf.Abs(currentSpeed - originalSpeed) > minSpeedOffsetValue)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, _returnSpeed);
            yield return new WaitForFixedUpdate();
        }

        currentSpeed = originalSpeed;
    }

    public void AddVelocity(Vector2 _velocity) {
        rb.velocity += _velocity;
    }

    public void SetVelocity(Vector2 _velocity) {
        rb.velocity = _velocity;
    }

    //switched to direction of the velocity
    public void SwitchVelocityDirection()
    {
        SetVelocity(GetVelocity * -1);
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    //adjusts the given dir with the current multiplier,
    //also only inverts a dir when it has changed
    public Vector2 AdjustDirToMultiplier(Vector2 _dir) {
        Vector2 directionDifference = direction - _dir;

        int multiplierDir = GetMultiplierDir();

        //only adjust the direction to multiplierDir if the direction has changed
        if (directionDifference.x != 0)
        {
            _dir.x *= multiplierDir;
        }
        if (directionDifference.y != 0)
        {
            _dir.y *= multiplierDir;
        }

        return _dir;
    }

    //set the direction without adjusting anything
    public void SetDirection(Vector2 _dir)
    {
        direction = _dir;
    }

    //returns the direction of the multiplier (1 or -1)
    public int GetMultiplierDir()
    {
        int speedMultiplierDir = 1;
        if (speedMultiplier < 0)
        {
            speedMultiplierDir = -1;
        }

        return speedMultiplierDir;
    }

    //returns our own controlled direction
    public Vector2 GetDirection() {
        return direction;
    }

    //returns the realtime direction of the velocity
    public Vector2 GetVelocityDirection() {
        return rb.velocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        Vector2 velocityNormalized = rb.velocity.normalized;
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
    }

    public void SetSpeed(float _newSpeed)
    {
        if(returnSpeedToOriginal != null)
        {
            StopCoroutine(returnSpeedToOriginal);
        }

        currentSpeed = _newSpeed;

        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public Vector2 GetVelocity {
        get { return rb.velocity; }
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    public float OriginalSpeed
    {
        get { return originalSpeed; }
    }

    public float CurrentSpeed {
        get { return currentSpeed; }
    }

    public float SpeedMultiplier
    {
        get { return speedMultiplier; }
        set { speedMultiplier = value; }
    }

    public bool CheckMovingStandard() {
        return updateDirectionalMovement != null;
    }
}

