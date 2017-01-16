using UnityEngine;
using System.Collections;
using System;

public class RevivedState : State, ITriggerer
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private int startDirectionRayLength = 50;

    [SerializeField]
    private int launchDelayWhenRespawning = 20;

    private CharScriptAccess _charAccess;

    private PlayerInputs _playerInputs;

    private PlayerActivateWeapon _playerActivateWeapon;

    private MoveTowards _moveTowards;

    private LookAt _lookAt;

    private AimRay _aimRay;

    //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool _isInPosition;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action ActivateTrigger { get; set; }
    public Action StopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _charAccess = GetComponent<CharScriptAccess>();

        _playerActivateWeapon = GetComponent<PlayerActivateWeapon>();
        _lookAt = gun.GetComponent<LookAt>();
        _aimRay = GetComponent<AimRay>();
        _moveTowards = GetComponent<MoveTowards>();
        _playerInputs = GetComponent<PlayerInputs>();
        GetComponent<ControlVelocity>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //make sure to reset the current touched input. If we are busy shooting, we reset the values and release;
        _playerInputs.ResetTouched();

        _playerActivateWeapon.SetWeaponInput(false);

        //Reset our movement
        _charAccess.ControlVelocity.SetVelocity(Vector2.zero);
        _charAccess.ControlVelocity.SetDirection(Vector2.zero);

        //wait a few frames so the player dont start moving immediatly if he panic clicked right after he respawned
        StartCoroutine(DelayLaunchingInput());
    }

    //delay the launching input for the following reasons:
    //the moment you die the player could be clicking, which would instantly launch the player once he is revived.
    //when we hit a checkpoint the player moves to the center, we must wait for the player to reach the center before he can fire himself
    IEnumerator DelayLaunchingInput()
    {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !_isInPosition)
        {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        SubscribeToAimInput();
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

    private void SubscribeToAimInput()
    {
        _playerInputs.Dragging += Aiming;
        _playerInputs.ReleaseInDir += LaunchInDir;
    }

    //while dragging, we point towards the given direction
    private void Aiming(Vector2 dir)
    {
        if (!_aimRay.AimRayActive)
        {
            _aimRay.StartAimRay((Vector2)transform.position + (dir * startDirectionRayLength));
        }

        _aimRay.RayDestination = (Vector2)transform.position + (dir * startDirectionRayLength);

        _lookAt.UpdateLookAt((Vector2)transform.position + dir);
    }

    //when we release, we launch ourselfs to the recieved direction
    private void LaunchInDir(Vector2 dir)
    {
        _playerActivateWeapon.SetWeaponInput(true);

        _aimRay.StopAimRay();
        _charAccess.SpeedMultiplier.MakeMultiplierPositive();
        _charAccess.ControlVelocity.SetDirection(dir * _charAccess.ControlVelocity.GetMultiplierDir());

        _charAccess.ControlSpeed.TempSpeedIncrease();

        EnterLaunchedState();
    }

    private void EnterLaunchedState()
    {
        if (StopTrigger != null)
            StopTrigger();

        _playerInputs.Dragging -= Aiming;
        _playerInputs.ReleaseInDir -= LaunchInDir;

        StateMachine.ActivateState(StateID.LaunchedState);
    }

    public bool IsInPosition
    {
        set { _isInPosition = value; }
    }
}