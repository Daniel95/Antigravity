using UnityEngine;
using System;
using System.Collections;

public class GrapplingState : State, ITriggerer {

    [SerializeField]
    private GameObject gun;

    private GrapplingHook grapplingHook;

    private CharScriptAccess charAccess;

    private PlayerInputs playerInputs;

    private LookAt gunLookAt;

    private Coroutine slingMovement;

    private Vector2 lastVelocity;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        grapplingHook = GetComponent<GrapplingHook>();
        charAccess = GetComponent<CharScriptAccess>();
        playerInputs = GetComponent<PlayerInputs>();
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();

        charAccess.controlSpeed.TempSpeedIncrease();

        //when we switch our speed, also switch the direction of our velocity
        charAccess.speedMultiplier.switchedMultiplier += FakeSwitchSpeed;

        //activate a different direction movement when in this state
        charAccess.controlVelocity.StopDirectionalMovement();
        slingMovement = StartCoroutine(SlingMovement());

        //exit the state when the grapple has released itself
        grapplingHook.stoppedGrappleLocking += ExitGrapple;

        //subscribe to the space input, so we know when to exit our grapple
        playerInputs.action += ExitGrapple;
    }

    IEnumerator SlingMovement()
    {
        while (true)
        {
            charAccess.controlVelocity.SetVelocity(lastVelocity = charAccess.controlVelocity.GetVelocityDirection() * (charAccess.controlVelocity.CurrentSpeed * Mathf.Abs(charAccess.controlVelocity.SpeedMultiplier)));

            yield return new WaitForFixedUpdate();
        }
    }

    private void FakeSwitchSpeed() {
        charAccess.controlVelocity.SwitchVelocityDirection();
    }

    public override void Act()
    {
        base.Act();

        if (charAccess.controlVelocity.GetVelocity == Vector2.zero && charAccess.controlVelocity.CurrentSpeed != 0) {
            charAccess.controlDirection.ApplyLogicDirection(charAccess.controlVelocity.GetVelocityDirection(), charAccess.collisionDirection.GetCurrentCollDir());
            EnterOnFootState();
        }

        //update the rotation of the gun
        gunLookAt.UpdateLookAt(grapplingHook.Destination);
    }

    private void ExitGrapple() {

        charAccess.controlVelocity.SetDirection(charAccess.controlVelocity.GetVelocityDirection());

        if (stopTrigger != null)
            stopTrigger();

        EnterLaunchedState();
    }

    private void EnterLaunchedState() {
        stateMachine.ActivateState(StateID.LaunchedState);
    }

    private void EnterOnFootState() {

        stateMachine.ActivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();

        //unsubscripte from all relevant delegates
        charAccess.speedMultiplier.switchedMultiplier -= FakeSwitchSpeed;
        grapplingHook.stoppedGrappleLocking -= ExitGrapple;
        playerInputs.action -= ExitGrapple;

        grapplingHook.ExitGrappleLock();

        //return to the normal movement
        StopCoroutine(slingMovement);
        charAccess.speedMultiplier.MakeMultiplierPositive();
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if(charAccess.controlTakeOff.CheckToBounce(collision))
        {
            charAccess.controlTakeOff.Bounce(lastVelocity.normalized, charAccess.collisionDirection.GetUpdatedCollDir(collision));
            EnterLaunchedState();
        }
        else
        {
            charAccess.controlDirection.ApplyLogicDirection(lastVelocity.normalized, charAccess.collisionDirection.GetUpdatedCollDir(collision));
            EnterOnFootState();
        }
    }
}
