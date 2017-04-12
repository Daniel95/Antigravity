using UnityEngine;
using System.Collections;
using System;
using IoCPlus;

public class RevivedStateView : View, ITriggerer
{
    [Inject] private EnableShootingInputEvent enableShootingInputEvent;

    [Inject] private ActivateFloatingStateEvent activateFloatingStateEvent;
    [Inject] private CancelDragInputEvent cancelDragInputEvent;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    [SerializeField]
    private int launchDelayWhenRespawning = 20;

    private CharScriptAccess _charAccess;

    private MoveTowards _moveTowards;

    private LookAt _lookAt;

    private AimRayView _aimRay;

    //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool _isInPosition;

    private bool canFire;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    public override void Initialize() {
        base.Initialize();

        cancelDragInputEvent.Dispatch();

        enableShootingInputEvent.Dispatch(false);

        _charAccess.ControlVelocity.SetVelocity(Vector2.zero);
        _charAccess.ControlVelocity.SetDirection(Vector2.zero);

        _charAccess.CollisionDirection.ResetCollisionDirection();

        StartCoroutine(DelayLaunchingInput());
    }

    public override void Dispose() {
        base.Dispose();

        if (StopTrigger != null)
            StopTrigger();
    }

    private void Awake()
    {
        _charAccess = GetComponent<CharScriptAccess>();

        _lookAt = gun.GetComponent<LookAt>();
        _aimRay = GetComponent<AimRayView>();
        _moveTowards = GetComponent<MoveTowards>();
        GetComponent<CharacterVelocityView>();
    }

    IEnumerator DelayLaunchingInput()
    {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !_isInPosition)
        {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        canFire = true;
    }

    public void StartMovingToCenterCheckPoint(Vector2 _checkPointPosition)
    {
        _moveTowards.ReachedDestination += ReachedPosition;
        _moveTowards.StartMoving(_checkPointPosition);
    }

    private void ReachedPosition()
    {
        _isInPosition = true;

        if (ActivateTrigger != null)
            ActivateTrigger();
    }

    public void Aim(Vector2 dir)
    {
        if (!_aimRay.AimRayActive)
        {
            _aimRay.StartAimRay((Vector2)transform.position + (dir * startDirectionRayLength));
        }

        _aimRay.RayDestination = (Vector2)transform.position + (dir * startDirectionRayLength);

        _lookAt.UpdateLookAt((Vector2)transform.position + dir);
    }

    public void Launch(Vector2 dir)
    {
        enableShootingInputEvent.Dispatch(true);

        _aimRay.StopAimRay();
        _charAccess.ControlVelocity.SetDirection(dir);

        _charAccess.ControlSpeed.TempSpeedIncrease();

        activateFloatingStateEvent.Dispatch();
    }

    public bool IsInPosition
    {
        set { _isInPosition = value; }
    }
}