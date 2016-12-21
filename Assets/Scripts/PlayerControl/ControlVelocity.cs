using UnityEngine;
using System;
using System.Collections;

public class ControlVelocity : MonoBehaviour {

    [SerializeField]
    private float speed = 3;

    private float speedMultiplier = 1;

    [SerializeField]
    private Vector2 direction;

    private Vector2 lastVelocity;

    private float originalTargetSpeed;

    private Rigidbody2D rb;

    private  Coroutine updateDirectionalMovement;

    private Coroutine updateNaturalMovement;

    private Coroutine addSpeedOverTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            lastVelocity = rb.velocity;

            //add our own constant force
            rb.velocity = direction * (speed * speedMultiplier);
            yield return new WaitForFixedUpdate();
        }
    }

    public void AddVelocity(Vector2 _velocity) {
        lastVelocity = rb.velocity;
        rb.velocity += _velocity;
    }

    public void SetVelocity(Vector2 _velocity) {
        lastVelocity = rb.velocity;
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

    public Vector2 GetLastVelocity
    {
        get { return lastVelocity; }
    }

    public float Speed {
        get { return speed; }
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

