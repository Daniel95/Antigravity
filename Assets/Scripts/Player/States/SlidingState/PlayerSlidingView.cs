using IoCPlus;
using System.Collections;
using UnityEngine;

public class PlayerSlidingView : CharacterSlidingView {

    [Inject] private PlayerStartRotatingAroundCornerEvent playerStartRotatingAroundCornerEvent;
    [Inject] private PlayerStopRotatingAroundCornerEvent playerStopRotatingAroundCornerEvent;

    [Inject] private PlayerJumpStatus playerJumpStatus;
    [Inject] private PlayerTurnStatus playerTurnStatus;

    [SerializeField] private float rotateSpeed = 90;

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterSliding> playerSlidingRef;
    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnRef;
    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    private Coroutine rotate90DegreesAroundPosition;

    public override void Initialize() {
        base.Initialize();
        playerSlidingRef.Set(this);
    }

    private void StartRotating90DegreesAroundPosition(Vector2 position) {
        if(rotate90DegreesAroundPosition != null) { return; }
        rotate90DegreesAroundPosition = StartCoroutine(Rotate90DegreesAroundPosition(position));
    }

    private IEnumerator Rotate90DegreesAroundPosition(Vector2 positionToRotateAround) {
        PlayerRotatingAroundCornerStatusView.Rotating = true;

        Vector2 targetOffset = positionToRotateAround - (Vector2)transform.position;
        Vector2 targetDirection = (targetOffset).normalized;
        Vector2 moveDirectionRight = Quaternion.AngleAxis(90, -Vector3.forward) * playerVelocityRef.Get().MoveDirection;

        bool targetIsToTheRight = VectorHelper.DirectionIsToTheRight(moveDirectionRight, targetDirection);

        playerVelocityRef.Get().DisableDirectionalMovement();
        playerVelocityRef.Get().SetVelocity(Vector2.zero);

        Vector3 rotateAxis = targetIsToTheRight ? -Vector3.forward : Vector3.forward;

        float startAngle = transform.rotation.eulerAngles.z;

        while (true) {
            float speed = playerVelocityRef.Get().CurrentSpeed;
            transform.RotateAround(positionToRotateAround, rotateAxis, (speed * rotateSpeed) * Time.deltaTime);

            float angleOffset = Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, startAngle));
            if (angleOffset >= 90) {
                break;
            }

            yield return null;
        }

        Vector2 newOffset = Quaternion.AngleAxis(90, (rotateAxis * -1)) * targetOffset;
        transform.position = positionToRotateAround + newOffset;

        transform.rotation = new Quaternion();

        Quaternion rotation = Quaternion.AngleAxis(90, rotateAxis);

        playerVelocityRef.Get().SetMoveDirection(VectorHelper.Round(rotation * playerVelocityRef.Get().MoveDirection));
        playerTurnRef.Get().SavedDirection = VectorHelper.Round(rotation * playerTurnRef.Get().SavedDirection);
        playerDirectionPointerRef.Get().PointToDirection(playerTurnRef.Get().SavedDirection);
        playerVelocityRef.Get().EnableDirectionalMovement();

        PlayerRotatingAroundCornerStatusView.Rotating = false;
        rotate90DegreesAroundPosition = null;
    }

    private void OnEnable() {
        OnAlignWithTarget += StartRotating90DegreesAroundPosition;
    }

    private void OnDisable() {
        OnAlignWithTarget -= StartRotating90DegreesAroundPosition;
    }

}
