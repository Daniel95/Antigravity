using UnityEngine;
using System.Collections;
using IoCPlus;
using System;

public class CharacterSlidingView : View, ICharacterSliding {

    public Transform CurrentTargetCornerTransform { get { return currentTargetCornerTransform; } }

    [Inject(Label.Player)] private Ref<ICharacterVelocity> characterVelocityRef;

    protected Action<Vector2> OnAlignWithTarget;

    private Coroutine checkAligningWithTargetCoroutine;
    private Transform currentTargetCornerTransform;
    private Vector2 cornerDirection;

    public void StartCheckingRotateAroundCornerConditions(Transform targetCornerTransform) {
        if (checkAligningWithTargetCoroutine != null) { return; }

        Vector2 moveDirection = characterVelocityRef.Get().MoveDirection;

        if(moveDirection.x != 0 && moveDirection.y != 0) { return; }

        cornerDirection = VectorHelper.Round(targetCornerTransform.rotation * Vector2.one);

        if (moveDirection.x != cornerDirection.x && moveDirection.y != cornerDirection.y) { return; }

        currentTargetCornerTransform = targetCornerTransform;
        checkAligningWithTargetCoroutine = StartCoroutine(CheckAligningWithPosition(targetCornerTransform.position));
    }

    public void StopCheckingRotateAroundCornerConditions() {
        if (checkAligningWithTargetCoroutine != null) {
            StopCoroutine(checkAligningWithTargetCoroutine);
            checkAligningWithTargetCoroutine = null;
        }
    }

    private IEnumerator CheckAligningWithPosition(Vector2 cornerPosition) {
        Vector2 moveDirection = characterVelocityRef.Get().MoveDirection;
        Vector2 halfWorldScale = VectorHelper.Abs(transform.localScale) / 2;
        Vector2 characterCornerOffset = VectorHelper.Multiply(halfWorldScale, moveDirection);

        while (true) {
            Vector2 characterCornerPosition = (Vector2)transform.position + characterCornerOffset;
            Vector2 offsetToCharacterCornerPosition = characterCornerPosition - cornerPosition;

            if (VectorHelper.VectorsAxisesHaveSameSigns(offsetToCharacterCornerPosition, cornerDirection)) {
                if (OnAlignWithTarget != null) {
                    OnAlignWithTarget(cornerPosition);
                }
                break;
            }

            yield return null;
        }

        currentTargetCornerTransform = null;
        checkAligningWithTargetCoroutine = null;
    }

}