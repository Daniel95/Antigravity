using UnityEngine;
using System.Collections;

public class PatrollingState : State
{
    private CharScriptAccess _charAccess;

    protected override void Start()
    {
        base.Start();
        _charAccess = GetComponent<CharScriptAccess>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _charAccess.ControlVelocity.StartDirectionalMovement();
    }

    private void ExitState()
    {
        StateMachine.ActivateState(StateID.GrapplingState);
    }

    public override void ResetState()
    {
        base.ResetState();
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
        }
    }
}