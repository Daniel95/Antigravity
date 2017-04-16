using IoCPlus;
using System;
using UnityEngine;

public class CharacterJumpView : View, ICharacterJump, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [Inject] private CharacterTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private CharacterTemporarySpeedIncreaseEvent characterTemporarySpeedIncreaseEvent;
    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;
    [Inject] private CharacterRemoveCollisionDirectionEvent characterRemoveCollisionDirectionEvent;
    [Inject] private CharacterJumpEvent characterJumpEvent;

    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float instantJumpStrength = 0.05f;
    [SerializeField] private int earlyJumpCoverFrames = 10;

    public Action TookOff;

    private bool isInBouncyTrigger;

    private Coroutine _retryJumpAfterDelay;

    private Frames frames;

    public override void Initialize() {
        characterJumpRef.Set(this);
    }

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
    private void Jump(CharacterJumpParameter characterJumpParameter) {
        characterTemporarySpeedChangeEvent.Dispatch(new CharacterTemporarySpeedChangeParameter(0.5f + jumpSpeedBoost));

        Vector2 newDirection = characterJumpParameter.MoveDirection;

        //check the raycastdir, our newDir is the opposite of one of the axes
        if (characterJumpParameter.CollisionDirection.x != 0) {
            newDirection.x = characterJumpParameter.CollisionDirection.x * -1;
        }
        else if (characterJumpParameter.CollisionDirection.y != 0) {
            newDirection.y = characterJumpParameter.CollisionDirection.y * -1;
        }

        if (characterJumpParameter.RaycastDirection.x == 0 || characterJumpParameter.RaycastDirection.y == 0) {
            transform.position += (Vector3)(newDirection * instantJumpStrength);
        }

        characterSetMoveDirectionEvent.Dispatch(newDirection);

        if (TookOff != null) {
            TookOff();
        }
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="directionInfo"></param>
    public void Bounce(CharacterDirectionParameter directionInfo) {
        if (directionInfo.CollisionDirection.x != 0 || directionInfo.CollisionDirection.y != 0) {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (directionInfo.CollisionDirection.x != 0) {
                directionInfo.MoveDirection.x *= -1;
            }
            if (directionInfo.CollisionDirection.y != 0) {
                directionInfo.MoveDirection.y *= -1;
            }

            characterSetMoveDirectionEvent.Dispatch(directionInfo.MoveDirection);

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
        frames = GetComponent<Frames>();
    }
}
