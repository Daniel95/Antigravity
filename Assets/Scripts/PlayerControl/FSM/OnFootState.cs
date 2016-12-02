using UnityEngine;
using System.Collections;

public class OnFootState : State {

    [SerializeField]
    private KeyCode SwitchGravityInput = KeyCode.Space;

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    protected override void Awake()
    {
        base.Awake();
        plrAccess = GetComponent<PlayerScriptAccess>();
        grapplingHook = GetComponent<GrapplingHook>();
    }

    public override void EnterState()
    {
        base.EnterState();

        plrAccess.controlVelocity.StopDirectionalMovement();
        plrAccess.controlVelocity.StartDirectionalMovement();

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
        plrAccess.controlDirection.CheckDirection(plrAccess.controlVelocity.GetControlledDirection());
    }
}
