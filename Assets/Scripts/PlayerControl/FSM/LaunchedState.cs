using UnityEngine;
using System.Collections;

public class LaunchedState : State
{

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    private FutureDirectionIndicator directionIndicator;

    //private Vector2 previousDirection;

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

        //previousDirection = plrAccess.controlVelocity.GetVelocityDirection();

        directionIndicator.PointToCeilVelocityDir();
    }

    private void EnterGrapplingState()
    {
        GeneralStateCleanUp();

        stateMachine.ActivateState(StateID.GrapplingState);
        stateMachine.DeactivateState(StateID.LaunchedState);
    }

    private void EnterOnFootState()
    {
        GeneralStateCleanUp();

        stateMachine.ActivateState(StateID.OnFootState);
        stateMachine.DeactivateState(StateID.LaunchedState);
    }

    private void GeneralStateCleanUp()
    {
        //unsubscripte from all relevant delegates
        grapplingHook.StartedGrappleLocking -= EnterGrapplingState;
    }

    public override void OnTrigEnter2D(Collider2D collider)
    {
        base.OnTrigEnter2D(collider);

        //only register a collision when the other collider isn't a trigger. we use our own main collider as a trigger
        if (!collider.isTrigger)
        {
            plrAccess.controlDirection.ActivateLogicDirection(plrAccess.controlVelocity.GetLastVelocity.normalized);

            EnterOnFootState();
        }
    }
}