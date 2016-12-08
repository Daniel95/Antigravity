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
        plrAccess.controlVelocity.StartDirectionalMovement();
    }

    public override void EnterState()
    {
        base.EnterState();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.StartedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        plrAccess.playerInputs.GetInputController().space += Jump;
    }

    private void Jump()
    {
        plrAccess.switchGravity.StartGravitating();
    }

    private void ExitState()
    {
        plrAccess.playerInputs.GetInputController().space -= Jump;

        grapplingHook.StartedGrappleLocking -= ExitState;
        stateMachine.ActivateState(StateID.GrapplingState);
        stateMachine.DeactivateState(StateID.OnFootState);
    }

    //on collision we check in which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        plrAccess.controlDirection.SetLogicDirection(plrAccess.controlVelocity.GetControlledDirection());
    }
}
