using IoCPlus;
using System;
using UnityEngine;

public class CharacterJumpView : View, ICharacterJump, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [Inject] private CharacterTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;
    [Inject] private CharacterRemoveCollisionDirectionEvent characterRemoveCollisionDirectionEvent;
    [Inject] private CharacterJumpEvent characterJumpEvent;
    [Inject] private CharacterRetryJumpEvent characterRetryJumpEvent;

    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float instantJumpStrength = 0.05f;
    [SerializeField] private int earlyJumpCoverFrames = 10;

    public Action TookOff;

    private Coroutine retryJumpAfterDelay;

    private Frames frames;

    public override void Initialize() {
        characterJumpRef.Set(this);
    }

    public void TryJump(CharacterJumpEvent.Parameter characterJumpParameter) {
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

    public void RetryJump(CharacterJumpEvent.Parameter characterJumpParameter)  {
        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (characterJumpParameter.CollisionDirection != Vector2.zero) {
            StopCoroutine(retryJumpAfterDelay);
            characterJumpEvent.Dispatch(characterJumpParameter);
        }
    }

    /// <summary>
    /// changes the direction of ControlVelocity, to create a jumping effect.
    /// </summary>
    public void Jump(CharacterJumpEvent.Parameter characterJumpParameter) {

        Vector2 newDirection = characterJumpParameter.MoveDirection;

        //check the raycastdir, our newDir is the opposite of one of the axes
        if (characterJumpParameter.CollisionDirection.x != 0) {
            newDirection.x = characterJumpParameter.CollisionDirection.x * -1;
        }
        else if (characterJumpParameter.CollisionDirection.y != 0) {
            newDirection.y = characterJumpParameter.CollisionDirection.y * -1;
        }

        if (characterJumpParameter.CenterRaycastDirection.x == 0 || characterJumpParameter.CenterRaycastDirection.y == 0) {
            transform.position += (Vector3)(newDirection * instantJumpStrength);
        }

        characterSetMoveDirectionEvent.Dispatch(newDirection);
        characterRemoveCollisionDirectionEvent.Dispatch(characterJumpParameter.CollisionDirection);
        characterTemporarySpeedChangeEvent.Dispatch(new CharacterTemporarySpeedChangeEvent.Parameter(0.5f + jumpSpeedBoost));

        if (TookOff != null) {
            TookOff();
        }
    }

    void Start() {
        frames = GetComponent<Frames>();
    }
}
