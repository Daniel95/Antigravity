using UnityEngine;
using System.Collections;

public class GrapplingState : State {

    [SerializeField]
    private int maxFramesStuck = 40;

    [SerializeField]
    private GameObject gun;

    private GrapplingHook grapplingHook;

    private PlayerScriptAccess plrAccess;

    private LookAt gunLookAt;

    private Coroutine slingMovement;

    private Vector2 direction;

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

        //when we switch our speed, also switch the direction of our velocity
        plrAccess.speedMultiplier.switchedMultiplier += FakeSwitchSpeed;

        //activate a different direction movement when in this state
        plrAccess.controlVelocity.StopDirectionalMovement();
        slingMovement = StartCoroutine(SlingMovement());

        //exit the state when the grapple has released itself
        grapplingHook.StoppedGrappleLocking += EnterLaunchedState;

        //subscribe to the space input, so we know when to exit our grapple
        plrAccess.playerInputs.GetInputController().space += ExitGrapple;

        StartCoroutine(CheckStuckTimer());
    }

    IEnumerator SlingMovement()
    {
        while (true)
        {
            direction = plrAccess.controlVelocity.GetVelocityDirection();
            plrAccess.controlVelocity.SetVelocity(direction * (plrAccess.controlVelocity.Speed * Mathf.Abs(plrAccess.controlVelocity.SpeedMultiplier)));

            yield return new WaitForFixedUpdate();
        }
    }

    private void FakeSwitchSpeed() {
        plrAccess.controlVelocity.SwitchVelocityDirection();
    }

    public override void Act()
    {
        base.Act();

        //update the rotation of the gun
        gunLookAt.UpdateLookAt(grapplingHook.Destination);
    }

    private void ExitGrapple() {
        EnterLaunchedState();
    }

    IEnumerator CheckStuckTimer()
    {
        int framesStuckCounter = 0;

        while (framesStuckCounter < maxFramesStuck)
        {
            framesStuckCounter++;
            yield return new WaitForFixedUpdate();
        }

        //check if the player is still stuck, if so unlock the grapple
        if (plrAccess.controlVelocity.GetVelocity == Vector2.zero)
        {
            plrAccess.controlDirection.SetLogicDirection(direction);
            EnterOnFootState();
        }
    }

    private void EnterLaunchedState() {
        GeneralStateCleanUp();

        stateMachine.ActivateState(StateID.LaunchedState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    private void EnterOnFootState() {
        GeneralStateCleanUp();

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    private void GeneralStateCleanUp()
    {
        //unsubscripte from all relevant delegates
        plrAccess.speedMultiplier.switchedMultiplier -= FakeSwitchSpeed;
        grapplingHook.StoppedGrappleLocking -= EnterLaunchedState;
        plrAccess.playerInputs.GetInputController().space -= ExitGrapple;

        grapplingHook.ExitGrappleLock();

        //return to the normal movement
        StopCoroutine(slingMovement);
        plrAccess.controlVelocity.SetDirection(direction = plrAccess.controlVelocity.GetVelocityDirection());
        plrAccess.speedMultiplier.MakeMultiplierPositive();

        //reactivate the normal movement when in exiting grappling state
        plrAccess.controlVelocity.StartDirectionalMovement();
    }

    //on collision we exit this state, and check which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        plrAccess.controlDirection.SetLogicDirection(direction);
        EnterOnFootState();
    }
}
