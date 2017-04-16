using IoCPlus;
using UnityEngine;

public class CharacterBounceView : View, ICharacterBounce {

    [Inject] private Ref<ICharacterBounce> characterBounceRef;

    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;

    private bool isInBouncyTrigger;

    public override void Initialize() {
        characterBounceRef.Set(this);
    }

    /// <summary>
    /// Changes the direction of ControlVelocity to create a bouncing effect.
    /// </summary>
    /// <param name="directionParameter"></param>
    public void Bounce(CharacterDirectionParameter directionParameter) {
        if (directionParameter.CollisionDirection.x != 0 || directionParameter.CollisionDirection.y != 0) {
            //check the raycastdir, our newDir is the opposite of one of the axes
            if (directionParameter.CollisionDirection.x != 0) {
                directionParameter.MoveDirection.x *= -1;
            }
            if (directionParameter.CollisionDirection.y != 0) {
                directionParameter.MoveDirection.y *= -1;
            }

            characterSetMoveDirectionEvent.Dispatch(directionParameter.MoveDirection);

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

    private void OnTriggerExit2D(Collider2D collision) {
        if (isInBouncyTrigger && collision.transform.CompareTag(Tags.Bouncy)) {
            isInBouncyTrigger = false;
        }
    }
}
