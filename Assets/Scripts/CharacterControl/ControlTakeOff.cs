using UnityEngine;
using System;
using System.Collections;

public class ControlTakeOff : MonoBehaviour, ITriggerer {

    [SerializeField]
    private float instaJumpStrength = 0.05f;

    private CharScriptAccess _plrAcces;

    private CharRaycasting _charRaycasting;

    public Action TookOff;

    private bool _inBouncyTrigger;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    // Use this for initialization
    void Start () {
        _plrAcces = GetComponent<CharScriptAccess>();

        _charRaycasting = GetComponent<CharRaycasting>();
    }


    /// <summary>
    /// changes the direction of ControlVelocity, to create a jumping effect.
    /// </summary>
    public void Jump()
    {
        if (stopTrigger != null) {
            stopTrigger();
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
        if (collisionDir.x == 0 || collisionDir.y == 0)
        {
            _plrAcces.ControlSpeed.TempSpeedIncrease();

            Vector2 newDir = _plrAcces.ControlVelocity.GetDirection();

            //check the raycastdir, our newDir is the opposite of one of the axes
            if (collisionDir.x != 0)
            {
                newDir.x = collisionDir.x * -1;
            }
            else if(collisionDir.y != 0)
            {
                newDir.y = collisionDir.y * -1;
            }

            _plrAcces.ControlVelocity.SetDirection(_plrAcces.ControlVelocity.AdjustDirToMultiplier(newDir));

            if(rayDir.x == 0 || rayDir.y == 0)
            {
                transform.position += (Vector3)(_plrAcces.ControlVelocity.GetDirection() * (instaJumpStrength * _plrAcces.ControlVelocity.SpeedMultiplier));
            }

            if (TookOff != null)
                TookOff();
        }
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="_currentDir"></param>
    /// <param name="_collisionDir"></param>
    public void Bounce(Vector2 _currentDir, Vector2 _collisionDir) {
        _plrAcces.ControlSpeed.TempSpeedIncrease();

        if (_collisionDir.x != 0 || _collisionDir.y != 0)
        {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (_collisionDir.x != 0)
            {
                _currentDir.x *= -1;
            }
            if (_collisionDir.y != 0)
            {
                _currentDir.y *= -1;
            }

            _plrAcces.ControlVelocity.SetDirection(_currentDir);

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
