using UnityEngine;
using System.Collections;

public class OnFootState : State {

    [SerializeField]
    private KeyCode SwitchGravityInput = KeyCode.Space;

    private ControlVelocity playerVelocity;
    private GrapplingHook grapplingHook;
    private ControlDirection playerDirection;
    private ControlGravity playerGravity;

    protected override void Awake()
    {
        base.Awake();
        playerVelocity = GetComponent<ControlVelocity>();
        grapplingHook = GetComponent<GrapplingHook>();
        playerDirection = GetComponent<ControlDirection>();
        playerGravity = GetComponent<ControlGravity>();
    }

    protected override void Start()
    {
        base.Start();

        playerVelocity.StartReturnSpeedToNormal(0.1f);
    }

    public override void EnterState()
    {
        base.EnterState();

        playerVelocity.StopDirectionalMovement();
        playerVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.StartedGrappleLocking += ExitState;

        playerVelocity.ResetMaxSpeed();
    }

    public override void Act()
    {
        base.Act();

        //switch the gravity
        if (Input.GetKeyDown(SwitchGravityInput))
        {
            playerGravity.SwitchGravity();
        }
    }

    private void ExitState()
    {
        grapplingHook.StartedGrappleLocking -= ExitState;
        stateMachine.ActivateState(StateID.GrapplingState);
        stateMachine.DeactivateState(StateID.OnFootState);
    }

    //on collision we check in which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        playerDirection.CheckDirection(playerVelocity.GetDirection);
    }
}
