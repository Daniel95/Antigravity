using UnityEngine;
using System.Collections;

public class LaunchedState : State
{
    private CharScriptAccess _charAccess;
    private GrapplingHook _grapplingHook;

    private FutureDirectionIndicator _directionIndicator;

    protected override void Awake()
    {
        base.Awake();
        _directionIndicator = GetComponent<FutureDirectionIndicator>();
        _charAccess = GetComponent<CharScriptAccess>();
        _grapplingHook = GetComponent<GrapplingHook>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //reactivate the normal movement
        _charAccess.ControlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        _grapplingHook.StartedGrappleLocking += EnterGrapplingState;

        _directionIndicator.PointToCeilVelocityDir();
    }

    private void EnterGrapplingState()
    {
        StateMachine.ActivateState(StateID.GrapplingState);
    }

    private void EnterOnFootState()
    {
        StateMachine.ActivateState(StateID.OnFootState);
    }

    public override void ResetState()
    {
        base.ResetState();

        //unsubscripte from all relevant delegates
        _grapplingHook.StartedGrappleLocking -= EnterGrapplingState;
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if (_charAccess.ControlTakeOff.CheckToBounce(collision))
        {
            _charAccess.ControlTakeOff.Bounce(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            _charAccess.ControlDirection.ApplyLogicDirection(_charAccess.ControlVelocity.GetDirection(), _charAccess.CollisionDirection.GetUpdatedCollDir(collision));

            EnterOnFootState();
        }
    }
}