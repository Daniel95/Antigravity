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

    private CharScriptAccess charAccess;

    private PlayerInputs playerInputs;

    private PlayerActivateWeapon playerActivateWeapon;

    private MoveTowards moveTowards;

    private LookAt lookAt;

    private AimRay aimRay;

    private bool bulletTimeActive;

    //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool isInPosition;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        charAccess = GetComponent<CharScriptAccess>();

        playerActivateWeapon = GetComponent<PlayerActivateWeapon>();
        lookAt = gun.GetComponent<LookAt>();
        aimRay = GetComponent<AimRay>();
        moveTowards = GetComponent<MoveTowards>();
        playerInputs = GetComponent<PlayerInputs>();
        GetComponent<ControlVelocity>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //make sure to reset the current touched input. If we are busy shooting, we reset the values and release;
        playerInputs.ResetTouched();

        playerActivateWeapon.SetWeaponInput(false);

        //Reset our movement
        charAccess.ControlVelocity.SetVelocity(Vector2.zero);
        charAccess.ControlVelocity.SetDirection(Vector2.zero);

        //wait a few frames so the player dont start moving immediatly if he panic clicked right after he respawned
        StartCoroutine(DelayLaunchingInput());
    }

    //delay the launching input for the following reasons:
    //the moment you die the player could be clicking, which would instantly launch the player once he is revived.
    //when we hit a checkpoint the player moves to the center, we must wait for the player to reach the center before he can fire himself
    IEnumerator DelayLaunchingInput()
    {
        int framesCounter = launchDelayWhenRespawning;

        while (framesCounter < 0 || !isInPosition)
        {
            framesCounter--;
            yield return new WaitForFixedUpdate();
        }

        SubscribeToAimInput();
    }

    public void StartMovingToCenterCheckPoint(Vector2 _checkPointPosition)
    {
        moveTowards.reachedDestination += ReachedPosition;
        moveTowards.StartMoving(_checkPointPosition);
    }

    private void ReachedPosition()
    {
        isInPosition = true;

        if (activateTrigger != null)
            activateTrigger();
    }

    private void SubscribeToAimInput()
    {
        playerInputs.dragging += Aiming;
        playerInputs.releaseInDir += LaunchInDir;
    }

    //while dragging, we point towards the given direction
    private void Aiming(Vector2 _dir)
    {
        if (!aimRay.AimRayActive)
        {
            aimRay.StartAimRay((Vector2)transform.position + (_dir * startDirectionRayLength));
        }

        aimRay.SetRayDestination = (Vector2)transform.position + (_dir * startDirectionRayLength);

        lookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    //when we release, we launch ourselfs to the recieved direction
    private void LaunchInDir(Vector2 _dir)
    {
        playerActivateWeapon.SetWeaponInput(true);

        aimRay.StopAimRay();
        charAccess.SpeedMultiplier.MakeMultiplierPositive();
        charAccess.ControlVelocity.SetDirection(_dir * charAccess.ControlVelocity.GetMultiplierDir());

        charAccess.ControlSpeed.TempSpeedIncrease();

        EnterLaunchedState();
    }

    private void EnterLaunchedState()
    {
        if (stopTrigger != null)
            stopTrigger();

        playerInputs.dragging -= Aiming;
        playerInputs.releaseInDir -= LaunchInDir;

        stateMachine.ActivateState(StateID.LaunchedState);
    }

    public bool IsInPosition
    {
        set { isInPosition = value; }
    }
}