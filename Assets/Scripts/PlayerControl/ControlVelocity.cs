using UnityEngine;
using System;
using System.Collections;

public class ControlVelocity : MonoBehaviour {

    [SerializeField]
    private float targetSpeed = 5;

    private float currentSpeed;

    private float speedMultiplier = 1;

    [SerializeField]
    private float speedIncreaseTime = 0.3f;

    [SerializeField]
    private float speedIncreaseMultiplier = 1.5f;

    [SerializeField]
    private float speedDecreaseTime = 0.1f;

    [SerializeField]
    private float speedDecreaseMultiplier = 0.5f;

    [SerializeField]
    private Vector2 direction;

    private float originalTargetSpeed;

    private Rigidbody2D rb;

    private  Coroutine updateDirectionalMovement;

    private Coroutine updateNaturalMovement;

    private Coroutine addSpeedOverTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalTargetSpeed = targetSpeed;
    }

    public void StartDirectionalMovement()
    {
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
            //add our own constant force, without removing the gravity of our rigidbodys
            rb.velocity = direction * (currentSpeed * speedMultiplier);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ReturnSpeedToNormal(float _time)
    {
        while (currentSpeed != targetSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, _time);
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartReturnSpeedToNormal(float _time)
    {
        StartCoroutine(ReturnSpeedToNormal(_time));
    }

    public void TempSlowDown() {
        currentSpeed = targetSpeed * speedDecreaseMultiplier;
        StartCoroutine(ReturnSpeedToNormal(speedDecreaseTime));
    }

    public void TempSpeedUp() {
        currentSpeed = targetSpeed * speedIncreaseMultiplier;
        StartCoroutine(ReturnSpeedToNormal(speedIncreaseTime));
    }

    public void StartIncrementingSpeed(float _increment, float _max)
    {
        addSpeedOverTime = StartCoroutine(IncrementingSpeed(_increment, _max));
    }


    public void StopIncrementingSpeed() {
        StopCoroutine(addSpeedOverTime);
    }


    IEnumerator IncrementingSpeed (float _increment, float _max)
    {
        if (_max > currentSpeed)
        {
            while (currentSpeed < _max)
            {
                currentSpeed += _increment;
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (currentSpeed > _max)
            {
                currentSpeed += _increment;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void AddVelocity(Vector2 _velocity) {
        rb.velocity += _velocity;
    }

    public void SetVelocity(Vector2 _velocity) {
        rb.velocity = _velocity;
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    //adjusts the given dir with the current multiplier,
    //also only inverts a dir when it has changed
    public void SetAdjustingDirection(Vector2 _dir) {
        Vector2 directionDifference = direction - _dir;

        int multiplierDir = GetMultiplierDir();

        //only adjust the direction to multiplierDir if it has changed
        if (directionDifference.x != 0) {
            _dir.x *= multiplierDir;
        }
        if (directionDifference.y != 0) {
            _dir.y *= multiplierDir;
        }

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

    //set the direction without adjusting anything
    public void SetDirectDirection(Vector2 _dir)
    {
        direction = _dir;
    }

    //returns our own controlled direction
    public Vector2 GetControlledDirection() {
        return direction;
    }

    //returns the realtime direction of the velocity
    public Vector2 GetVelocityDirection() {
        return GetVelocity.normalized;
    }

    public Vector2 GetCeilVelocityDirection() {
        Vector2 velocityNormalized = GetVelocity.normalized;
        return new Vector2(Rounding.InvertOnNegativeCeil(velocityNormalized.x), Rounding.InvertOnNegativeCeil(velocityNormalized.y));
    }

    public Vector2 GetVelocity {
        get { return rb.velocity; }
    }

    public float TargetSpeed
    {
        get { return targetSpeed; }
        set { targetSpeed = value; }
    }

    public float CurrentSpeed {
        get { return currentSpeed; }
    }

    public float SpeedMultiplier
    {
        get { return speedMultiplier; }
        set { speedMultiplier = value; }
    }

    public void ResetTargetSpeed()
    {
        targetSpeed = originalTargetSpeed;
    }
}

