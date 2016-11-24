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
    }

    protected override void Start()
    {
        base.Start();
        playerVelocity.StartReturnSpeedToNormal(0.1f);
    }

    public override void EnterState()
    {
        base.EnterState();
        grapplingHook.StartedGrappleLocking += ExitState;
        playerVelocity.StartPlayerMovement();
    }

    public override void Act()
    {
        base.Act();
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

    public override void OnCollEnter2D(Collision2D coll)
    {
        playerDirection.CheckDirection();
    }
}
