using UnityEngine;
using System.Collections;

public class LaunchedState : State
{

    private CharScriptAccess charAccess;
    private GrapplingHook grapplingHook;

    private FutureDirectionIndicator directionIndicator;

    protected override void Awake()
    {
        base.Awake();
        directionIndicator = GetComponent<FutureDirectionIndicator>();
        charAccess = GetComponent<CharScriptAccess>();
        grapplingHook = GetComponent<GrapplingHook>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //reactivate the normal movement
        charAccess.controlVelocity.StartDirectionalMovement();

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

        if (charAccess.controlTakeOff.CheckToBounce(collision))
        {
            charAccess.controlTakeOff.Bounce(charAccess.controlVelocity.GetDirection(), charAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            charAccess.controlDirection.ApplyLogicDirection(charAccess.controlVelocity.GetDirection(), charAccess.collisionDirection.GetUpdatedCollDir(collision));

            EnterOnFootState();
        }
    }
}