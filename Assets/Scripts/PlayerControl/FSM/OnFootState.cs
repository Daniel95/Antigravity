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
        grapplingHook.startedGrappleLocking += ExitState;

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
    }

    public override void ResetState()
    {
        base.ResetState();

        plrAccess.playerInputs.InputController.action -= Jump;

        grapplingHook.startedGrappleLocking -= ExitState;
    }

    public override void OnTriggerEnterCollider(Collider2D collider)
    {
        base.OnTriggerEnterCollider(collider);

        plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetDirection());
    }
}
