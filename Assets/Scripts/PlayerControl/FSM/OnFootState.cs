using UnityEngine;
using System.Collections;

public class OnFootState : State {

    [SerializeField]
    private KeyCode SwitchGravityInput = KeyCode.Space;

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    protected override void Start()
    {
        base.Start();
        plrAccess = GetComponent<PlayerScriptAccess>();
        grapplingHook = GetComponent<GrapplingHook>();
        plrAccess.controlVelocity.StartDirectionalMovement();
    }

    public override void EnterState()
    {
        base.EnterState();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.StartedGrappleLocking += ExitState;
    }

    public override void Act()
    {
        base.Act();

        //switch the gravity
        if (Input.GetKeyDown(SwitchGravityInput))
        {
            plrAccess.switchGravity.StartGravitating();
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
        plrAccess.controlDirection.SetLogicDirection(plrAccess.controlVelocity.GetControlledDirection());
    }
}
