using IoCPlus;
using UnityEngine;

public class PatrollingView : View {

    public override void Initialize() {
        base.Initialize();
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