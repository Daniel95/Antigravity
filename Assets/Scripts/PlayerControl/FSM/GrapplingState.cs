using UnityEngine;
using System;
using System.Collections;

public class GrapplingState : State, ITriggerer {

    [SerializeField]
    private GameObject gun;

    private GrapplingHook grapplingHook;

    private PlayerScriptAccess plrAccess;

    private LookAt gunLookAt;

    private Coroutine slingMovement;

    private Vector2 direction;

    //used by action trigger to decide when to start the instructions/tutorial, and when to stop it
    public Action activateTrigger { get; set; }
    public Action stopTrigger { get; set; }

    protected override void Awake()
    {
        base.Awake();

        grapplingHook = GetComponent<GrapplingHook>();
        plrAccess = GetComponent<PlayerScriptAccess>();
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();

        plrAccess.controlSpeed.TempSpeedIncrease();

        //when we switch our speed, also switch the direction of our velocity
        plrAccess.speedMultiplier.switchedMultiplier += FakeSwitchSpeed;

        //activate a different direction movement when in this state
        plrAccess.controlVelocity.StopDirectionalMovement();
        slingMovement = StartCoroutine(SlingMovement());

        //exit the state when the grapple has released itself
        grapplingHook.stoppedGrappleLocking += EnterLaunchedState;

        //subscribe to the space input, so we know when to exit our grapple
        plrAccess.playerInputs.InputController.action += ExitGrapple;

    }

    IEnumerator SlingMovement()
    {
        while (true)
        {
            plrAccess.controlVelocity.SetVelocity(plrAccess.controlVelocity.GetVelocityDirection() * (plrAccess.controlVelocity.CurrentSpeed * Mathf.Abs(plrAccess.controlVelocity.SpeedMultiplier)));

            yield return new WaitForFixedUpdate();
        }
    }

    private void FakeSwitchSpeed() {
        plrAccess.controlVelocity.SwitchVelocityDirection();
    }

    public override void Act()
    {
        base.Act();

        if (plrAccess.controlVelocity.GetVelocity == Vector2.zero && plrAccess.controlVelocity.CurrentSpeed != 0) {
            plrAccess.controlDirection.ActivateLogicDirection(direction);
            EnterOnFootState();
        }

        //update the rotation of the gun
        gunLookAt.UpdateLookAt(grapplingHook.Destination);
    }

    private void ExitGrapple() {

        if (stopTrigger != null)
            stopTrigger();

        plrAccess.controlVelocity.SetDirection(direction = plrAccess.controlVelocity.GetVelocityDirection());
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
        plrAccess.speedMultiplier.switchedMultiplier -= FakeSwitchSpeed;
        grapplingHook.stoppedGrappleLocking -= EnterLaunchedState;
        plrAccess.playerInputs.InputController.action -= ExitGrapple;

        grapplingHook.ExitGrappleLock();

        //return to the normal movement
        StopCoroutine(slingMovement);
        plrAccess.speedMultiplier.MakeMultiplierPositive();

        //reactivate the normal movement when in exiting grappling state
        plrAccess.controlVelocity.StartDirectionalMovement();
    }

    public override void OnTriggerEnterCollider(Collider2D collider)
    {
        base.OnTriggerEnterCollider(collider);

        if (collider.CompareTag(Tags.Bouncy))
        {
            plrAccess.controlVelocity.SetDirection(plrAccess.controlVelocity.GetVelocityDirection());
            EnterLaunchedState();
        }
        else
        {
            plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetDirection());
            EnterOnFootState();
        }
    }
}
