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
    }

    private void EnterOnFootState()
    {
        stateMachine.ActivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();
        //unsubscripte from all relevant delegates
        grapplingHook.StartedGrappleLocking -= EnterGrapplingState;
    }

    public override void OnTriggerEnterCollider(Collider2D collider)
    {
        base.OnTriggerEnterCollider(collider);

        if(!collider.CompareTag(Tags.Bouncy))
        {
            plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetLastVelocity.normalized);

            EnterOnFootState();
        }
    }
}