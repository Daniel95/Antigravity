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
    [Inject] private CharacterRetryJumpEvent characterRetryJumpEvent;

    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float instantJumpStrength = 0.05f;
    [SerializeField] private int earlyJumpCoverFrames = 10;

    public Action TookOff;

    private bool isInBouncyTrigger;

    private Coroutine retryJumpAfterDelay;

    private Frames frames;

    public override void Initialize() {
        characterJumpRef.Set(this);
    }

    public void TryJump(CharacterJumpParameter characterJumpParameter) {
        if (StopTrigger != null) {
            StopTrigger();
        }

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (characterJumpParameter.CollisionDirection != Vector2.zero) {
            if (retryJumpAfterDelay != null) {
                StopCoroutine(retryJumpAfterDelay);
            }
            characterJumpEvent.Dispatch(characterJumpParameter);
        } else {
            retryJumpAfterDelay = frames.ExecuteAfterDelay(earlyJumpCoverFrames, () => characterRetryJumpEvent.Dispatch());
        }
    }

    public void RetryJump(CharacterJumpParameter characterJumpParameter)  {
        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (characterJumpParameter.CollisionDirection != Vector2.zero) {
            StopCoroutine(retryJumpAfterDelay);
            characterJumpEvent.Dispatch(characterJumpParameter);
        }
    }

    /// <summary>
    /// changes the direction of ControlVelocity, to create a jumping effect.
    /// </summary>
    public void Jump(CharacterJumpParameter characterJumpParameter) {
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

    public bool CheckBounce(Collision2D collision) {
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

    void Start() {
        frames = GetComponent<Frames>();
    }
}
