using UnityEngine;
using System.Collections;

public class OnFootState : State {

    private CharScriptAccess _charAccess;
    private PlayerInputs _playerInputs;
    private GrapplingHook _grapplingHook;

    protected override void Awake()
    {
        base.Awake();
        _grapplingHook = GetComponent<GrapplingHook>();
        _playerInputs = GetComponent<PlayerInputs>();
    }

    protected override void Start()
    {
        base.Start();
        _charAccess = GetComponent<CharScriptAccess>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //reactivate the normal movement
        _charAccess.ControlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        _grapplingHook.StartedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        _playerInputs.Jump += Jump;
    }

    private void Jump()
    {
        _charAccess.ControlTakeOff.Jump();
    }

    private void ExitState()
    {
        StateMachine.ActivateState(StateID.GrapplingState);
    }

    public override void ResetState()
    {
        base.ResetState();

        _playerInputs.Jump -= Jump;

        _grapplingHook.StartedGrappleLocking -= ExitState;
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
