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

    private Rigidbody2D rb;

    private Coroutine updateMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartPlayerMovement()
    {
        updateMovement = StartCoroutine(UpdateMovement());
    }

    public void StopPlayerMovement()
    {
        StopCoroutine(updateMovement);
    }

    IEnumerator UpdateMovement()
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

    public void SetVelocityX(float _xVelocity) {
        rb.velocity = new Vector3(_xVelocity, rb.velocity.y);
    }

    public void SetVelocityY(float _yVelocity) {
        rb.velocity = new Vector3(rb.velocity.x, _yVelocity);
    }

    public void SwitchDirection() {
        direction *= -1;
    }

    public void SetDirection(Vector2 _dir) {
        direction = _dir;
    }
    
    public Vector2 GetDirection {
        get { return direction; }
    }

    public Vector2 GetVelocity {
        get { return rb.velocity; }
    }
}

