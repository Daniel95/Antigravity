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

    private Vector2 previousFrameDirection;

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
        plrAccess.speedMultiplier.switchedMultiplier += plrAccess.controlVelocity.SwitchVelocityDirection;

        //activate a different direction movement when in this state
        plrAccess.controlVelocity.StopDirectionalMovement();
        slingMovement = StartCoroutine(SlingMovement());
        plrAccess.controlVelocity.StartDirectionalMovement();

        //exit the state when the grapple has released itself
        grapplingHook.StoppedGrappleLocking += EnterLaunchedState;

        StartCoroutine(CheckStuckTimer());
    }

    IEnumerator SlingMovement() {
        while (true) {
            plrAccess.controlVelocity.SetDirection(previousFrameDirection = plrAccess.controlVelocity.GetVelocityDirection());
            plrAccess.controlVelocity.SpeedMultiplier = Mathf.Abs(plrAccess.controlVelocity.SpeedMultiplier);

            yield return new WaitForFixedUpdate();
        }
    }

    public override void Act()
    {
        base.Act();

        if (plrAccess.playerInputs.CheckJumpInput())
        {
            EnterLaunchedState();
        }

        //update the rotation of the gun
        gunLookAt.UpdateLookAt(grapplingHook.Destination);
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

        plrAccess.speedMultiplier.ResetSpeedMultiplier();

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    private void GeneralStateCleanUp()
    {
        StopCoroutine(slingMovement);

        //unsubscripte from all relevant delegates
        plrAccess.speedMultiplier.switchedMultiplier -= plrAccess.controlVelocity.SwitchVelocityDirection;
        grapplingHook.StoppedGrappleLocking -= EnterLaunchedState;

        grapplingHook.ExitGrappleLock();
    }

    //on collision we exit this state, and check which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        plrAccess.controlDirection.SetLogicDirection(previousFrameDirection);
        EnterOnFootState();
    }
}
