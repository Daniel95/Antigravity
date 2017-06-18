using IoCPlus;
using System;
using UnityEngine;

public class CharacterJumpView : View, ICharacterJump, ITriggerer {

    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    [Inject] private Ref<ICharacterJump> characterJumpRef;

    [Inject] private PlayerTemporarySpeedChangeEvent characterTemporarySpeedChangeEvent;
    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;
    [Inject] private CharacterRemoveCollisionDirectionEvent characterRemoveCollisionDirectionEvent;
    [Inject] private CharacterJumpEvent characterJumpEvent;
    [Inject] private CharacterRetryJumpEvent characterRetryJumpEvent;

    [SerializeField] private float jumpSpeedBoost = 0.3f;
    [SerializeField] private float instantJumpStrength = 0.05f;
    [SerializeField] private int earlyJumpCoverFrames = 10;

    private Coroutine retryJumpAfterDelayCoroutine;

    private Frames frames;

    public override void Initialize() {
        characterJumpRef.Set(this);
    }

    public void TryJump(CharacterJumpEvent.Parameter characterJumpParameter) {
        if (StopTrigger != null) {
            StopTrigger();
        }

        if (characterJumpParameter.CollisionDirection != Vector2.zero) {
            if (retryJumpAfterDelayCoroutine != null) {
                frames.StopDelayExecute(retryJumpAfterDelayCoroutine);
            }
            characterJumpEvent.Dispatch(characterJumpParameter);
        } else {
            retryJumpAfterDelayCoroutine = frames.ExecuteAfterDelay(earlyJumpCoverFrames, () => characterRetryJumpEvent.Dispatch());
        }
    }

    public void RetryJump(CharacterJumpEvent.Parameter characterJumpParameter)  {
        if (characterJumpParameter.CollisionDirection != Vector2.zero) {
            frames.StopDelayExecute(retryJumpAfterDelayCoroutine);
            characterJumpEvent.Dispatch(characterJumpParameter);
        }
    }

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
        characterTemporarySpeedChangeEvent.Dispatch(new PlayerTemporarySpeedChangeEvent.Parameter(0.5f + jumpSpeedBoost));
    }

    void Start() {
        frames = GetComponent<Frames>();
    }
}
