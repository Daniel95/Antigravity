using UnityEngine;
using System.Collections;

public class GrapplingState : State {

    [SerializeField]
    private KeyCode cancelGrappleKey = KeyCode.Space;

    [SerializeField]
    private float grappleSpeed = 3;

    [SerializeField]
    private int maxFramesStuck = 40;

    private ControlVelocity playerVelocity;
    private ControlDirection playerDirection;
    private GrapplingHook grapplingHook;

    private Coroutine checkStuckTimer;

    [SerializeField]
    private GameObject gun;

    private LookAt gunLookAt;

    private Coroutine updateSlingDirection;

    private Vector2 previousDirection;

    protected override void Awake()
    {
        base.Awake();
        playerVelocity = GetComponent<ControlVelocity>();
        playerDirection = GetComponent<ControlDirection>();
        grapplingHook = GetComponent<GrapplingHook>();
        gunLookAt = gun.GetComponent<LookAt>();
    }

    public override void EnterState()
    {
        base.EnterState();
        playerVelocity.TargetSpeed = grappleSpeed;

        //activate a different direction movement when in this state
        ActivateSlingDirection();

        //exit the state when the grapple has released itself
        grapplingHook.StoppedGrappleLocking += ExitState;

        checkStuckTimer = StartCoroutine(CheckStuckTimer());
    }

    //set the right slide direction so we always move the same speed when we slide
    private void ActivateSlingDirection() {
        //stop the directionalMovement
        playerVelocity.StopDirectionalMovement();

        updateSlingDirection = StartCoroutine(UpdateSlingDirection());

        //start the directionalMovement again when we updated the direction
        playerVelocity.StartDirectionalMovement();
    }

    //updates the direction of the controlVelocity script so that we swing smoothly towards the right direction
    IEnumerator UpdateSlingDirection()
    {
        while (true)
        {
            //set the direction to the direction of our current velocity, 
            //also save it in previous direction so next frame we can check what the old direction was
            playerVelocity.SetDirection(previousDirection = playerVelocity.GetVelocityDirection);
            yield return new WaitForFixedUpdate();
        }
    }

    public override void Act()
    {
        base.Act();

        if (Input.GetKeyDown(cancelGrappleKey))
        {
            ExitState();
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
        if (playerVelocity.GetVelocity == Vector2.zero)
        {
            playerVelocity.SetDirection(grapplingHook.Direction);
            ExitState();
        }
    }

    private void ExitState() {
        //unsubscripte from all relevant delegates
        grapplingHook.StartedGrappleLocking -= ActivateSlingDirection;
        grapplingHook.StoppedGrappleLocking -= ExitState;

        grapplingHook.ExitGrappleLock();

        //stop the updateslingdir coroutine and unsubscribe from the started grappling delegate 
        StopCoroutine(updateSlingDirection);

        grapplingHook.StoppedGrappleLocking -= ExitState;

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.GrapplingState);
    }

    //on collision we exit this state, and check which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        playerDirection.CheckDirection(previousDirection);
        ExitState();
    }
}
