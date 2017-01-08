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

        //reactivate the normal movement
        plrAccess.controlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.startedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        plrAccess.playerInputs.InputController.action += Jump;
    }

    private void Jump()
    {
        plrAccess.controlTakeOff.Jump();
    }

    private void ExitState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
    }

    public override void ResetState()
    {
        base.ResetState();

        plrAccess.playerInputs.InputController.action -= Jump;

        grapplingHook.startedGrappleLocking -= ExitState;
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if(plrAccess.controlTakeOff.CheckToBounce(collision))
        {
            plrAccess.controlTakeOff.Bounce(plrAccess.controlVelocity.GetDirection(), plrAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            plrAccess.controlDirection.ApplyLogicDirection(plrAccess.controlVelocity.GetDirection(), plrAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
    }
}
