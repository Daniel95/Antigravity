using UnityEngine;
using System.Collections;
using IoCPlus;
using System;

public class CharacterSlidingView : View, ICharacterSliding {

    [Inject(Label.Player)] private Ref<ICharacterVelocity> characterVelocityRef;

    protected Action<Vector2> OnAlignWithTarget;

    private Coroutine checkAligningWithTargetCoroutine;

    public void StartCheckingRotateAroundCornerConditions(Vector2 cornerPosition) {
        if (checkAligningWithTargetCoroutine != null) { return; }

        //Debug.Log("__________");
        //Debug.Log("player position " + transform.position);
        //Debug.Log("cornerPosition position " + cornerPosition);

        Vector2 moveDirection = characterVelocityRef.Get().MoveDirection;

        if(moveDirection.x != 0 && moveDirection.y != 0) {
            Debug.LogError("Not sliding");
            return;
        }

        Vector2 halfWorldScale = (Vector2)transform.lossyScale / 2;
        Vector2 characterCorner = (Vector2)transform.position + VectorHelper.Multiply(halfWorldScale, moveDirection);
        Vector2 cornersOffset = cornerPosition - characterCorner;

        //Debug.Log("moveDirection " + moveDirection);
        //Debug.Log("cornersOffset " + cornersOffset);

        if (!CheckDirectionAxisesHaveSameSign(moveDirection, cornersOffset)) {
            Debug.Log("Axises DON'T have same direction");
            Debug.Log("Stop Checking Rotate");
            return;
        }

        checkAligningWithTargetCoroutine = StartCoroutine(CheckAligningWithPosition(cornerPosition));
    }

    public void StopCheckingRotateAroundCornerConditions() {
        if (checkAligningWithTargetCoroutine != null) {
            StopCoroutine(checkAligningWithTargetCoroutine);
            checkAligningWithTargetCoroutine = null;
        }
    }

    private IEnumerator CheckAligningWithPosition(Vector2 cornerPosition) {
        Vector2 startOffset = cornerPosition - (Vector2)transform.position;
        while (true) {
            Vector2 offset = cornerPosition - (Vector2)transform.position;
            if (!MathHelper.HasSameSign(offset.x, startOffset.x) || !MathHelper.HasSameSign(offset.y, startOffset.y)) {
                if(OnAlignWithTarget != null) {
                    OnAlignWithTarget(cornerPosition);
                }
                break;
            }
            yield return null;
        }
        checkAligningWithTargetCoroutine = null;
    }

    private bool CheckDirectionAxisesHaveSameSign(Vector2 direction1, Vector2 direction2) {
        if(direction1.x != 0) {
            if(!MathHelper.HasSameSign(direction1.x, direction2.x)) {
                return false;
            }
        }
        if (direction1.y != 0) {
            if (!MathHelper.HasSameSign(direction1.y, direction2.y)) {
                return false;
            }
        }
        return true;
    }

}