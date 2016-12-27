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

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.StartedGrappleLocking += EnterGrapplingState;

        directionIndicator.PointToCeilVelocityDir();
    }

    private void EnterGrapplingState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
        stateMachine.DeactivateState(StateID.LaunchedState);
    }

    private void EnterOnFootState()
    {
        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.LaunchedState);
    }

    public override void ResetState()
    {
        base.ResetState();
        //unsubscripte from all relevant delegates
        grapplingHook.StartedGrappleLocking -= EnterGrapplingState;
    }

    public override void OnTrigEnter2D(Collider2D collider)
    {
        base.OnTrigEnter2D(collider);

        //only register a collision when the other collider isn't a trigger. we use our own main collider as a trigger
        if (!collider.isTrigger && !collider.CompareTag(Tags.Bouncy))
        {
            plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetLastVelocity.normalized);

            EnterOnFootState();
        }
    }
}