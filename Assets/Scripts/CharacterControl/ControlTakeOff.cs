using UnityEngine;
using System;
using System.Collections;

public class ControlTakeOff : MonoBehaviour, ITriggerer {

    [SerializeField]
    private float jumpSpeedBoost = 0.3f;

    [SerializeField]
    private float jumpSpeedReturn = 0.01f;

    [SerializeField]
    private float instaJumpStrength = 0.05f;

    [SerializeField]
    private int earlyJumpCoverFrames = 10;

    private CharScriptAccess _plrAcces;

    private CharRaycasting _charRaycasting;

    private Frames _frames;

    public Action TookOff;

    private bool _inBouncyTrigger;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    // Use this for initialization
    void Start () {
        _plrAcces = GetComponent<CharScriptAccess>();
        _charRaycasting = GetComponent<CharRaycasting>();
        _frames = GetComponent<Frames>();
    }

    /// <summary>
    /// Check if we can jump, if so, activate Jump()
    /// </summary>
    public void TryJump()
    {
        if (StopTrigger != null)
        {
            StopTrigger();
        }

        Vector2 collisionDir = _plrAcces.CollisionDirection.GetCurrentCollDir();
        Vector2 rayDir = new Vector2(_charRaycasting.CheckHorizontalMiddleDir(), _charRaycasting.CheckVerticalMiddleDir());

        //if collisiondir is zero, it may be because we are barely not colliding, while it looks like we are.
        //as a backup plan we use raycasting if this happens so we can still jump
        if (collisionDir == Vector2.zero)
        {
            collisionDir = rayDir;
        }

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionDir != Vector2.zero)
        {
            _frames.StopExecuteAfterDelay();
            Jump(collisionDir, rayDir);
        }
        else //try to jump again a few frames later, so it will still respond even if the player pressed jump too early.
        {
            _frames.ExecuteAfterDelay(earlyJumpCoverFrames, TryJump);
        }
    }

    /// <summary>
    /// changes the direction of ControlVelocity, to create a jumping effect.
    /// </summary>
    private void Jump(Vector2 collisionDir, Vector2 rayDir)
    {
        _plrAcces.ControlSpeed.TempSpeedChange(0.5f + jumpSpeedBoost);

        Vector2 newDir = _plrAcces.ControlVelocity.GetDirection();

        //check the raycastdir, our newDir is the opposite of one of the axes
        if (collisionDir.x != 0)
        {
            newDir.x = collisionDir.x * -1;
        }
        else if (collisionDir.y != 0)
        {
            newDir.y = collisionDir.y * -1;
        }

        _plrAcces.ControlVelocity.SetDirection(_plrAcces.ControlVelocity.AdjustDirToMultiplier(newDir));

        if (rayDir.x == 0 || rayDir.y == 0)
        {
            transform.position += (Vector3)(_plrAcces.ControlVelocity.GetDirection() * (instaJumpStrength * _plrAcces.ControlVelocity.SpeedMultiplier));
        }

        if (TookOff != null)
            TookOff();
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collisionDir"></param>
    public void Bounce(Vector2 currentDir, Vector2 collisionDir) {
        _plrAcces.ControlSpeed.TempSpeedIncrease();

        if (collisionDir.x != 0 || collisionDir.y != 0)
        {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (collisionDir.x != 0)
            {
                currentDir.x *= -1;
            }
            if (collisionDir.y != 0)
            {
                currentDir.y *= -1;
            }

            _plrAcces.ControlVelocity.SetDirection(currentDir);

            if (TookOff != null)
                TookOff();
        }
    }

    /// <summary>
    /// Check if we should bounce.
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    public bool CheckToBounce(Collision2D collision)
    {
        return collision.collider.CompareTag(Tags.Bouncy) || _inBouncyTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_inBouncyTrigger && collision.CompareTag(Tags.Bouncy))
        {
            //if we hit a trigger we say that we are currently in a bouncy trigger, in case we hit a non trigger collider
            _inBouncyTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_inBouncyTrigger && collision.transform.CompareTag(Tags.Bouncy))
        {
            _inBouncyTrigger = false;
        }
    }
}
