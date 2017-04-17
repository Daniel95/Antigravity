using UnityEngine;
using System.Collections;
using System;
using IoCPlus;

public class RevivedStateView : View, ITriggerer
{
    [Inject] private EnableShootingInputEvent enableShootingInputEvent;

    [Inject] private ActivateFloatingStateEvent activateFloatingStateEvent;
    [Inject] private CancelDragInputEvent cancelDragInputEvent;
    [Inject] private CharacterSetMoveDirectionEvent characterSetMoveDirectionEvent;

    [SerializeField] private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    [SerializeField]
    private int launchDelayWhenRespawning = 20;

    private MoveTowards moveTowards;
    private LookAt lookAt;
    private bool isInPosition; //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool canFire;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public override void Initialize() {
        base.Initialize();
        StartCoroutine(DelayLaunchingInput());
    }

    public override void Dispose() {
        base.Dispose();

        if (StopTrigger != null) {
            StopTrigger();
        }
    }

    private void Awake() {
        lookAt = gun.GetComponent<LookAt>();
        moveTowards = GetComponent<MoveTowards>();
        GetComponent<CharacterVelocityView>();
    }

    IEnumerator DelayLaunchingInput() {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !isInPosition) {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        canFire = true;
    }

    public void StartMovingToCenterCheckPoint(Vector2 _checkPointPosition) {
        moveTowards.ReachedDestination += ReachedPosition;
        moveTowards.StartMoving(_checkPointPosition);
    }

    private void ReachedPosition() {
        isInPosition = true;

        if (ActivateTrigger != null) {
            ActivateTrigger();
        }
    }

    public void Aim(Vector2 dir){
        if (!_aimRay.AimLineActive) {
            _aimRay.StartAimLine((Vector2)transform.position + (dir * startDirectionRayLength));
        }

        _aimRay.LineDestination = (Vector2)transform.position + (dir * startDirectionRayLength);

        lookAt.UpdateLookAt((Vector2)transform.position + dir);
    }

    public void Launch(Vector2 dir) {
        enableShootingInputEvent.Dispatch(true);

        _aimRay.StopAimLine();
        _charAccess.ControlVelocity.SetDirection(dir);

        _charAccess.ControlSpeed.TempSpeedIncrease();

        activateFloatingStateEvent.Dispatch();
    }

    public bool IsInPosition {
        set { isInPosition = value; }
    }
}