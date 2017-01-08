using UnityEngine;
using System.Collections;

public class LaunchedState : State
{

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    private FutureDirectionIndicator directionIndicator;

    protected override void Awake()
    {
        base.Awake();
        directionIndicator = GetComponent<FutureDirectionIndicator>();
        plrAccess = GetComponent<PlayerScriptAccess>();
        grapplingHook = GetComponent<GrapplingHook>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //reactivate the normal movement
        plrAccess.controlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.startedGrappleLocking += EnterGrapplingState;

        directionIndicator.PointToCeilVelocityDir();
    }

    private void EnterGrapplingState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
    }

    private void EnterOnFootState()
    {
        stateMachine.ActivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();

        //unsubscripte from all relevant delegates
        grapplingHook.startedGrappleLocking -= EnterGrapplingState;
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if (plrAccess.controlTakeOff.CheckToBounce(collision))
        {
            plrAccess.controlTakeOff.Bounce(plrAccess.controlVelocity.GetDirection(), plrAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            plrAccess.controlDirection.ApplyLogicDirection(plrAccess.controlVelocity.GetDirection(), plrAccess.collisionDirection.GetUpdatedCollDir(collision));

            EnterOnFootState();
        }
    }
}