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

        charAccess.ControlSpeed.TempSpeedIncrease();

        //when we switch our speed, also switch the direction of our velocity
        charAccess.SpeedMultiplier.SwitchedMultiplier += FakeSwitchSpeed;

        //activate a different direction movement when in this state
        charAccess.ControlVelocity.StopDirectionalMovement();
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
            charAccess.ControlVelocity.SetVelocity(lastVelocity = charAccess.ControlVelocity.GetVelocityDirection() * (charAccess.ControlVelocity.CurrentSpeed * Mathf.Abs(charAccess.ControlVelocity.SpeedMultiplier)));

            yield return new WaitForFixedUpdate();
        }
    }

    private void FakeSwitchSpeed() {
        charAccess.ControlVelocity.SwitchVelocityDirection();
    }

    public override void Act()
    {
        base.Act();

        if (charAccess.ControlVelocity.GetVelocity == Vector2.zero && charAccess.ControlVelocity.CurrentSpeed != 0) {
            charAccess.ControlDirection.ApplyLogicDirection(charAccess.ControlVelocity.GetVelocityDirection(), charAccess.CollisionDirection.GetCurrentCollDir());
            EnterOnFootState();
        }

        //update the rotation of the gun
        gunLookAt.UpdateLookAt(grapplingHook.Destination);
    }

    private void ExitGrapple() {

        charAccess.ControlVelocity.SetDirection(charAccess.ControlVelocity.GetVelocityDirection());

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
        charAccess.SpeedMultiplier.SwitchedMultiplier -= FakeSwitchSpeed;
        grapplingHook.stoppedGrappleLocking -= ExitGrapple;
        playerInputs.action -= ExitGrapple;

        grapplingHook.ExitGrappleLock();

        //return to the normal movement
        StopCoroutine(slingMovement);
        charAccess.SpeedMultiplier.MakeMultiplierPositive();
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if(charAccess.ControlTakeOff.CheckToBounce(collision))
        {
            charAccess.ControlTakeOff.Bounce(lastVelocity.normalized, charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            EnterLaunchedState();
        }
        else
        {
            charAccess.ControlDirection.ApplyLogicDirection(lastVelocity.normalized, charAccess.CollisionDirection.GetUpdatedCollDir(collision));
            EnterOnFootState();
        }
    }
}
