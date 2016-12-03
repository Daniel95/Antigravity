using UnityEngine;
using System.Collections;

public class GrapplingState : State {

    [SerializeField]
    private KeyCode cancelGrappleKey = KeyCode.Space;

    [SerializeField]
    private int maxFramesStuck = 40;

    private GrapplingHook grapplingHook;

    private FutureDirectionIndicator dirIndicator;

    private PlayerScriptAccess plrAccess;

    [SerializeField]
    private GameObject gun;

    private LookAt gunLookAt;

    private Coroutine updateSlingDirection;

    private Vector2 previousDirection;

    protected override void Awake()
    {
        base.Awake();
        grapplingHook = GetComponent<GrapplingHook>();
        plrAccess = GetComponent<PlayerScriptAccess>();
        dirIndicator = GetComponent<FutureDirectionIndicator>();
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //activate a different direction movement when in this state
        ActivateSlingDirection();

        plrAccess.changeSpeedMultiplier.switchedSpeed += FlipSwingDirection;
        
        //only flip the swingdir when our multiplier is in the minus, because we cant
        if (plrAccess.controlVelocity.SpeedMultiplier < 0) {
            FlipSwingDirection();
        }

        //exit the state when the grapple has released itself
        grapplingHook.StoppedGrappleLocking += StopGrappling;
        //grapplingHook.StoppedGrappleLocking += ExitState;

        StartCoroutine(CheckStuckTimer());
    }

    //set the right slide direction so we always move the same speed when we slide
    private void ActivateSlingDirection() {
        //stop the directionalMovement
        plrAccess.controlVelocity.StopDirectionalMovement();

        updateSlingDirection = StartCoroutine(UpdateSlingDirection());

        //start the directionalMovement again when we updated the direction
        plrAccess.controlVelocity.StartDirectionalMovement();
    }

    //updates the direction of the controlVelocity script so that we swing smoothly towards the right direction
    IEnumerator UpdateSlingDirection()
    {
        while (true)
        {
            //set the direction to the direction of our current velocity, 
            //also save it in previous direction so next frame we can check what the old direction was
            plrAccess.controlVelocity.SetDirection(previousDirection = plrAccess.controlVelocity.GetVelocityDirection());
            plrAccess.controlVelocity.SpeedMultiplier = Mathf.Abs(plrAccess.controlVelocity.SpeedMultiplier);
            yield return new WaitForFixedUpdate();
        }
    }

    //sets the velocity to the opposite
    private void FlipSwingDirection() {
        plrAccess.controlVelocity.SetVelocity(plrAccess.controlVelocity.GetVelocity * -1);
    }

    public override void Act()
    {
        base.Act();

        if (Input.GetKeyDown(cancelGrappleKey))
        {
            //ExitState();
            StopGrappling();
            dirIndicator.PointToCeilVelocityDir();
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
            plrAccess.controlVelocity.SetDirection(plrAccess.controlVelocity.AdjustDirToMultiplier(grapplingHook.Direction));
            ExitState();
        }
    }

    private void StopGrappling() {
        grapplingHook.StartedGrappleLocking -= ActivateSlingDirection;
        grapplingHook.StoppedGrappleLocking -= StopGrappling;

        grapplingHook.ExitGrappleLock();
    }

    private void ExitState() {
        //unsubscripte from all relevant delegates
        plrAccess.changeSpeedMultiplier.switchedSpeed -= FlipSwingDirection;
        grapplingHook.StartedGrappleLocking -= ActivateSlingDirection;
        grapplingHook.StoppedGrappleLocking -= ExitState;

        grapplingHook.ExitGrappleLock();

        plrAccess.changeSpeedMultiplier.ResetSpeedMultiplier();

        //stop the updateslingdir coroutine and unsubscribe from the started grappling delegate 
        StopCoroutine(updateSlingDirection);

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    //on collision we exit this state, and check which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        plrAccess.controlDirection.CheckDirection(previousDirection);
        ExitState();
    }
}
