using UnityEngine;
using System;
using System.Collections;
using IoCPlus;

public class CharacterJumpView : View, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<ICharacterVelocity> characterVelocityRef;

    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float instaJumpStrength = 0.05f;
    [SerializeField] private int earlyJumpCoverFrames = 10;

    public Action TookOff;

    private bool isInBouncyTrigger;

    private Coroutine _retryJumpAfterDelay;

    private CharacterRaycasting charaterRaycasting;
    private Frames frames;
    private CollisionDirectionDetection collisionDirectionDetection;

    public void TryJump() {
        if (StopTrigger != null) {
            StopTrigger();
        }

        CollisionInfo collisionInfo = GetCollisionInfo();

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionInfo.CollisionDirection != Vector2.zero) {
            if (_retryJumpAfterDelay != null) {
                StopCoroutine(_retryJumpAfterDelay);
            }
            Jump(collisionInfo.CollisionDirection, collisionInfo.RayDirection);
        } else {
            _retryJumpAfterDelay = frames.ExecuteAfterDelay(earlyJumpCoverFrames, RetryJump);
        }
    }

    private void RetryJump()  {
        CollisionInfo collisionInfo = GetCollisionInfo();

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (collisionInfo.CollisionDirection != Vector2.zero) {
            StopCoroutine(_retryJumpAfterDelay);
            Jump(collisionInfo.CollisionDirection, collisionInfo.RayDirection);
        }
    }
    
    private CollisionInfo GetCollisionInfo()  {
        Vector2 collisionDir = collisionDirectionDetection.CollisionDirection.GetCurrentCollDir();
        Vector2 rayDir = new Vector2(charaterRaycasting.CheckHorizontalMiddleDir(), charaterRaycasting.CheckVerticalMiddleDir());

        //if collisiondir is zero, it may be because we are barely not colliding, while it looks like we are.
        //as a backup plan we use raycasting if this happens so we can still jump
        if (collisionDir == Vector2.zero) {
            collisionDir = rayDir;
        }

        return new CollisionInfo(collisionDir, rayDir);
    }

    /// <summary>
    /// changes the direction of ControlVelocity, to create a jumping effect.
    /// </summary>
    private void Jump(Vector2 collisionDir, Vector2 rayDir) {
        _plrAcces.ControlSpeed.TempSpeedChange(0.5f + jumpSpeedBoost);

        Vector2 newDir = _plrAcces.ControlVelocity.GetDirection();

        //check the raycastdir, our newDir is the opposite of one of the axes
        if (collisionDir.x != 0) {
            newDir.x = collisionDir.x * -1;
        }
        else if (collisionDir.y != 0) {
            newDir.y = collisionDir.y * -1;
        }

        characterVelocityRef.SetDirection(newDir);

        if (rayDir.x == 0 || rayDir.y == 0) {
            transform.position += (Vector3)(_plrAcces.ControlVelocity.GetDirection() * instaJumpStrength);
        }

        collisionDirectionDetection.CollisionDirection.RemoveCollisionDirection(collisionDir);

        if (TookOff != null) {
            TookOff();
        }
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="currentDir"></param>
    /// <param name="collisionDir"></param>
    public void Bounce(DirectionParameter directionInfo) {
        _plrAcces.ControlSpeed.TempSpeedIncrease();

        if (directionInfo.CollisionDirection.x != 0 || directionInfo.CollisionDirection.y != 0) {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (directionInfo.CollisionDirection.x != 0) {
                directionInfo.MoveDirection.x *= -1;
            }
            if (directionInfo.CollisionDirection.y != 0) {
                directionInfo.MoveDirection.y *= -1;
            }

            _plrAcces.ControlVelocity.SetDirection(directionInfo.MoveDirection);

            if (TookOff != null) {
                TookOff();
            }
        }
    }

    public bool CheckToBounce(Collision2D collision) {
        return collision.collider.CompareTag(Tags.Bouncy) || isInBouncyTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (!isInBouncyTrigger && collision.CompareTag(Tags.Bouncy)) {
            isInBouncyTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInBouncyTrigger && collision.transform.CompareTag(Tags.Bouncy)) {
            isInBouncyTrigger = false;
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

    void Start() {
        charaterRaycasting = GetComponent<CharacterRaycasting>();
        frames = GetComponent<Frames>();
        collisionDirectionDetection = GetComponent<CollisionDirectionDetection>();
    }
}
