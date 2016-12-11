using UnityEngine;
using System.Collections;

public class LaunchedState : State
{

    private PlayerScriptAccess plrAccess;
    private GrapplingHook grapplingHook;

    private FutureDirectionIndicator directionIndicator;

    private Vector2 previousDirection;

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

        previousDirection = plrAccess.controlVelocity.GetVelocityDirection();

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

    //on collision we exit this state, and check which direction we should go
    public override void OnCollEnter2D(Collision2D coll)
    {
        plrAccess.controlDirection.SetLogicDirection(previousDirection);
        EnterOnFootState();
    }
}