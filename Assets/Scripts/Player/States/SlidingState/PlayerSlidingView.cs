using IoCPlus;
using System.Collections;
using UnityEngine;

public class PlayerSlidingView : CharacterSlidingView {

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
        Vector2 targetDirection = (positionToRotateAround - (Vector2)transform.position).normalized;
        bool targetIsToTheRight = VectorHelper.DirectionIsToTheRight(transform.right, targetDirection);


        playerVelocityRef.Get().DisableDirectionalMovement();

        Vector3 rotateAxis = targetIsToTheRight ? -Vector3.forward : Vector3.forward;

        float startAngle = transform.rotation.eulerAngles.z;

        Debug.Log("targetDirection " + targetDirection);
        Debug.Log("transform.right " + transform.right);
        Debug.Log("targetIsToTheRight " + targetIsToTheRight);
        Debug.Log("rotateAxis " + rotateAxis);

        while (true) {
            float speed = playerVelocityRef.Get().CurrentSpeed;
            transform.RotateAround(positionToRotateAround, rotateAxis, (rotateSpeed * speed) * Time.deltaTime);

            float angleOffset = Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, startAngle));
            if (angleOffset >= 90) {
                break;
            }

            yield return null;
        }

        /*
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        float roundedZAngle = RoundingHelper.RoundTo(eulerAngles.z, 90);
        Vector3 roundedAngles = new Vector3(eulerAngles.x, eulerAngles.y, roundedZAngle);
        transform.rotation = Quaternion.Euler(roundedAngles);
        */
        transform.rotation = new Quaternion();

        Quaternion rotation = Quaternion.AngleAxis(90, rotateAxis);

        playerVelocityRef.Get().SetMoveDirection(VectorHelper.Round(rotation * playerVelocityRef.Get().MoveDirection));
        playerTurnRef.Get().SavedDirection = VectorHelper.Round(rotation * playerTurnRef.Get().SavedDirection);
        playerDirectionPointerRef.Get().PointToDirection(playerTurnRef.Get().SavedDirection);
        playerVelocityRef.Get().EnableDirectionalMovement();

        rotate90DegreesAroundPosition = null;
    }

    private void OnEnable() {
        OnAlignWithTarget += StartRotating90DegreesAroundPosition;
    }

    private void OnDisable() {
        OnAlignWithTarget -= StartRotating90DegreesAroundPosition;
    }

}
