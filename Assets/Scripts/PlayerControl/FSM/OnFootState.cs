using UnityEngine;
using System.Collections;

public class OnFootState : State {

    private CharScriptAccess charAccess;
    private PlayerInputs playerInputs;
    private GrapplingHook grapplingHook;

    protected override void Awake()
    {
        base.Awake();
        grapplingHook = GetComponent<GrapplingHook>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    protected override void Start()
    {
        base.Start();
        charAccess = GetComponent<CharScriptAccess>();
    }

    public override void EnterState()
    {
        base.EnterState();

        //reactivate the normal movement
        charAccess.ControlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.startedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        playerInputs.action += Jump;
    }

    private void Jump()
    {
        charAccess.ControlTakeOff.Jump();
    }

    private void ExitState()
    {
        stateMachine.ActivateState(StateID.GrapplingState);
    }

    public override void ResetState()
    {
        base.ResetState();

        playerInputs.action -= Jump;

        grapplingHook.startedGrappleLocking -= ExitState;
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
