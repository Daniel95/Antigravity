using UnityEngine;
using System;
using System.Collections;

public class ControlVelocity : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 5;

    private float speed;

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

    private float startMaxSpeed;

    private Rigidbody2D rb;

    public Coroutine updateDirectionalMovement;

    public Coroutine updateNaturalMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startMaxSpeed = maxSpeed;
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
            rb.velocity = direction * speed;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ReturnSpeedToNormal(float _time)
    {
        while (speed != maxSpeed)
        {
            speed = Mathf.Lerp(speed, maxSpeed, _time);
            yield return new WaitForFixedUpdate();
        }
    }

    public void StartReturnSpeedToNormal(float _time)
    {
        StartCoroutine(ReturnSpeedToNormal(_time));
    }

    public void TempSlowDown() {
        speed = maxSpeed * speedDecreaseMultiplier;
        StartCoroutine(ReturnSpeedToNormal(speedDecreaseTime));
    }

    public void TempSpeedUp() {
        speed = maxSpeed * speedIncreaseMultiplier;
        StartCoroutine(ReturnSpeedToNormal(speedIncreaseTime));
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

    public void SetDirection(Vector2 _dir) {
        direction = _dir;
    }
    
    //returns our own controlled direction
    public Vector2 GetDirection {
        get { return direction; }
    }

    //returns the realtime direction of the velocity
    public Vector2 GetVelocityDirection {
        //get { return transform.InverseTransformDirection(rb.velocity).normalized; }
        get { return rb.velocity.normalized; }
    }

    public Vector2 GetVelocity {
        get { return rb.velocity; }
    }

    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    public void ResetMaxSpeed()
    {
        maxSpeed = startMaxSpeed;
    }
}

