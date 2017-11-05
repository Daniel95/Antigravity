using IoCPlus;
using System.Collections;
using UnityEngine;

public class PlayerRotateAroundCornerView : CharacterRotateAroundCornerView {

    [Inject] private PlayerStartedRotatingAroundCornerEvent playerStartRotatingAroundCornerEvent;
    [Inject] private PlayerStoppedRotatingAroundCornerEvent playerStopRotatingAroundCornerEvent;

    [Inject] private PlayerJumpStatus playerJumpStatus;
    [Inject] private PlayerTurnStatus playerTurnStatus;

    [SerializeField] private float rotateSpeed = 90;

    [Inject(Label.Player)] private Ref<ICharacterVelocity> playerVelocityRef;
    [Inject(Label.Player)] private Ref<ICharacterRotateAroundCorner> playerSlidingRef;
    [Inject(Label.Player)] private Ref<ICharacterTurnDirection> playerTurnRef;
    [Inject(Label.Player)] private Ref<ICharacterDirectionPointer> playerDirectionPointerRef;

    private Coroutine rotateAroundCornerCoroutine;
    private float startZRotation;

    public override void Initialize() {
        base.Initialize();
        playerSlidingRef.Set(this);
    }

    public override void CancelRotatingAroundCorner() {
        if (rotateAroundCornerCoroutine == null) { return; }
        StopCoroutine(rotateAroundCornerCoroutine);
        rotateAroundCornerCoroutine = null;

        float zRotation = transform.rotation.eulerAngles.z;
        transform.rotation = new Quaternion();

        float angleOffset = Mathf.DeltaAngle(startZRotation, zRotation);
        float angleDifference = Mathf.Abs(angleOffset);

        if (angleDifference > 45) {
            Vector3 rotationAxis = angleOffset < 0 ? -Vector3.forward : Vector3.forward;
            Quaternion rotation = Quaternion.AngleAxis(90, rotationAxis);

            Vector2 moveDirection = VectorHelper.Round(rotation * playerVelocityRef.Get().MoveDirection);
            playerVelocityRef.Get().SetMoveDirection(moveDirection);

            Vector2 savedDirection = VectorHelper.Round(rotation * playerTurnRef.Get().SavedDirection);
            playerTurnRef.Get().SavedDirection = savedDirection;
            playerDirectionPointerRef.Get().PointToDirection(savedDirection);
        }

        PlayerRotateAroundCornerStatusView.Rotating = false;
    }

    private void StartRotatingAroundCorner(Vector2 cornerPosition) {
        if(rotateAroundCornerCoroutine != null) { return; }
        startZRotation = transform.eulerAngles.z;
        rotateAroundCornerCoroutine = StartCoroutine(Rotate90DegreesAroundPosition(cornerPosition));
        PlayerRotateAroundCornerStatusView.Rotating = true;
    }

    private IEnumerator Rotate90DegreesAroundPosition(Vector2 positionToRotateAround) {
        Vector2 targetOffset = positionToRotateAround - (Vector2)transform.position;
        Vector2 targetDirection = (targetOffset).normalized;
        Vector2 moveDirectionRight = Quaternion.AngleAxis(90, -Vector3.forward) * playerVelocityRef.Get().MoveDirection;

        bool targetIsToTheRight = VectorHelper.DirectionIsToTheRight(moveDirectionRight, targetDirection);

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

        rotateAroundCornerCoroutine = null;
        PlayerRotateAroundCornerStatusView.Rotating = false;
    }

    private void OnEnable() {
        OnAlignWithTarget += StartRotatingAroundCorner;
    }

    private void OnDisable() {
        OnAlignWithTarget -= StartRotatingAroundCorner;
    }

}
