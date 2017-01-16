using UnityEngine;
using System.Collections;

public class PatrollingState : State
{
    private CharScriptAccess charAccess;

    protected override void Start()
    {
        base.Start();
        charAccess = GetComponent<CharScriptAccess>();
    }

    public override void EnterState()
    {
        base.EnterState();

        charAccess.ControlVelocity.StartDirectionalMovement();
    }

    private void ExitState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
    }

    public override void ResetState()
    {
        base.ResetState();
    }

    public override void OnCollEnter(Collision2D collision)
    {
        base.OnCollEnter(collision);

        if (charAccess.ControlTakeOff.CheckToBounce(collision))
        {
            charAccess.ControlTakeOff.Bounce(charAccess.ControlVelocity.GetDirection(), charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            charAccess.ControlDirection.ApplyLogicDirection(charAccess.ControlVelocity.GetDirection(), charAccess.CollisionDirection.GetUpdatedCollDir(collision));
        }
    }
}