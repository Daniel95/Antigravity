using UnityEngine;
using System.Collections;

public class OnFootState : State {

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    protected override void Awake()
    {
        base.Awake();
        grapplingHook = GetComponent<GrapplingHook>();
    }

    protected override void Start()
    {
        base.Start();
        plrAccess = GetComponent<PlayerScriptAccess>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.StartedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        plrAccess.playerInputs.InputController.action += Jump;
    }

    private void Jump()
    {
        plrAccess.switchGravity.Jump();
    }

    private void ExitState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
        stateMachine.DeactivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();

        plrAccess.playerInputs.InputController.action -= Jump;

        grapplingHook.StartedGrappleLocking -= ExitState;
    }

    public override void OnTrigEnter2D(Collider2D collider)
    {
        //only register a collision when the other collider isn't a trigger. we use our own main collider as a trigger
        if (!collider.isTrigger) {
            plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetDirection());
        }

        base.OnTrigEnter2D(collider);
    }
}
