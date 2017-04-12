using UnityEngine;
using System;
using System.Collections;
using IoCPlus;

public class CharacterJumpView : View, ITriggerer {

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [SerializeField]
    private float jumpSpeedBoost = 0.3f;

    [SerializeField]
    private float instaJumpStrength = 0.05f;

    [SerializeField]
    private int earlyJumpCoverFrames = 10;

    public Action TookOff;

    private CharScriptAccess _plrAcces;

    private CharacterRaycastingView _charRaycasting;

    private Frames _frames;

    private bool _inBouncyTrigger;

    private Coroutine _retryJumpAfterDelay;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    // Use this for initialization
    void Start () {
        _plrAcces = GetComponent<CharScriptAccess>();
        _charRaycasting = GetComponent<CharacterRaycastingView>();
        _frames = GetComponent<Frames>();
    }

    /// <summary>
    /// Check if we can jump, if so, activate Jump(), else we will RetryJump a few frames later.
    /// </summary>
    public void TryJump()
    {
        if (StopTrigger != null)
        {
            StopTrigger();
        }

        CollisionInfo collisionInfo = GetCollisionInfo();

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionInfo.CollisionDirection != Vector2.zero)
        {
            if (_retryJumpAfterDelay != null) {
                StopCoroutine(_retryJumpAfterDelay);
            }
            Jump(collisionInfo.CollisionDirection, collisionInfo.RayDirection);
        }
        else //retry to jump again a few frames later, so it will still respond even if the player pressed jump too early.
        {
            _retryJumpAfterDelay = _frames.ExecuteAfterDelay(earlyJumpCoverFrames, RetryJump);
        }
    }

    /// <summary>
    /// Retry to jump if we couldn't jump last time.
    /// </summary>
    private void RetryJump() 
    {
        CollisionInfo collisionInfo = GetCollisionInfo();

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionInfo.CollisionDirection != Vector2.zero)
        {
            StopCoroutine(_retryJumpAfterDelay);
            Jump(collisionInfo.CollisionDirection, collisionInfo.RayDirection);
        }
    }
    
    private CollisionInfo GetCollisionInfo() 
    {
        Vector2 collisionDir = _plrAcces.CollisionDirection.GetCurrentCollDir();
        Vector2 rayDir = new Vector2(_charRaycasting.CheckHorizontalMiddleDir(), _charRaycasting.CheckVerticalMiddleDir());

        //if collisiondir is zero, it may be because we are barely not colliding, while it looks like we are.
        //as a backup plan we use raycasting if this happens so we can still jump
        if (collisionDir == Vector2.zero)
        {
            collisionDir = rayDir;
        }

        return new CollisionInfo(collisionDir, rayDir);
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

        characterVelocityRef.SetDirection(newDir);

        if (rayDir.x == 0 || rayDir.y == 0)
        {
            transform.position += (Vector3)(_plrAcces.ControlVelocity.GetDirection() * instaJumpStrength);
        }

        _plrAcces.CollisionDirection.RemoveCollisionDirection(collisionDir);

        if (TookOff != null)
            TookOff();
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collisionDir"></param>
    public void Bounce(DirectionInfo directionInfo) {
        _plrAcces.ControlSpeed.TempSpeedIncrease();

        if (directionInfo.collisionDirection.x != 0 || directionInfo.collisionDirection.y != 0)
        {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (directionInfo.collisionDirection.x != 0)
            {
                directionInfo.moveDirection.x *= -1;
            }
            if (directionInfo.collisionDirection.y != 0)
            {
                directionInfo.moveDirection.y *= -1;
            }

            _plrAcces.ControlVelocity.SetDirection(directionInfo.moveDirection);

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

    private class CollisionInfo {
        public readonly Vector2 CollisionDirection;
        public readonly Vector2 RayDirection;

        public CollisionInfo(Vector2 collisionDirection, Vector2 rayDirection) {
            CollisionDirection = collisionDirection;
            RayDirection = rayDirection;
        }
    }
}
