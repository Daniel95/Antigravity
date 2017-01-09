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

        charAccess.controlVelocity.StartDirectionalMovement();
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

        if (charAccess.controlTakeOff.CheckToBounce(collision))
        {
            charAccess.controlTakeOff.Bounce(charAccess.controlVelocity.GetDirection(), charAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
        else
        {
            charAccess.controlDirection.ApplyLogicDirection(charAccess.controlVelocity.GetDirection(), charAccess.collisionDirection.GetUpdatedCollDir(collision));
        }
    }
}