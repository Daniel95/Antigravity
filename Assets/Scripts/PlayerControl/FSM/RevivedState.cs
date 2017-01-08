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

    private PlayerScriptAccess plrAccess;

    private ActivateWeapon activateWeapon;

    private MoveTowards moveTowards;

    private LookAt lookAt;

    private BulletTime bulletTime;

    private bool bulletTimeActive;

    //the first time we hit a checkpoint, we will be moving towards the center of it, once we reached the center we are in position and can fire ourselfes.
    private bool isInPosition;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        plrAccess = GetComponent<PlayerScriptAccess>();
        
        activateWeapon = GetComponent<ActivateWeapon>();
        lookAt = gun.GetComponent<LookAt>();
        bulletTime = GetComponent<BulletTime>();
        moveTowards = GetComponent<MoveTowards>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //make sure to reset the current touched input. If we are busy shooting, we reset the values and release;
        plrAccess.playerInputs.InputController.ResetTouched();

        //deactivate the weapon so we no longer shoot when we click/tab
        activateWeapon.enabled = false;

        //set our direction to zero so our velocityController doesn't move us
        plrAccess.controlVelocity.SetDirection(Vector2.zero);

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
        plrAccess.playerInputs.InputController.dragging += Dragging;
        plrAccess.playerInputs.InputController.release += Release;
    }

    //while dragging, we point towards the given direction
    private void Dragging(Vector2 _dir)
    {
        if (!bulletTime.BulletTimeActive)
        {
            bulletTime.StartBulletTime();
        }

        bulletTime.SetRayDestination = (Vector2)transform.position + (_dir * startDirectionRayLength);

        lookAt.UpdateLookAt((Vector2)transform.position + _dir);
    }

    //when we release, we launch ourselfs to the recieved direction
    private void Release(Vector2 _dir)
    {
        activateWeapon.enabled = true;

        bulletTime.StopBulletTime();
        plrAccess.speedMultiplier.MakeMultiplierPositive();
        plrAccess.controlVelocity.SetDirection(_dir * plrAccess.controlVelocity.GetMultiplierDir());

        plrAccess.controlSpeed.TempSpeedIncrease();

        EnterLaunchedState();
    }

    private void EnterLaunchedState()
    {
        if (stopTrigger != null)
            stopTrigger();

        plrAccess.playerInputs.InputController.dragging -= Dragging;
        plrAccess.playerInputs.InputController.release -= Release;

        stateMachine.ActivateState(StateID.LaunchedState);
    }

    public bool IsInPosition
    {
        set { isInPosition = value; }
    }
}