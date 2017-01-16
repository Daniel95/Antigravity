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
        charAccess.controlVelocity.StartDirectionalMovement();

        //subscribe to StartedGrappleLocking, so we know when we should start grappling and exit this state
        grapplingHook.startedGrappleLocking += ExitState;

        //subscribe to the space input, so we know when to jump
        playerInputs.action += Jump;
    }

    private void Jump()
    {
        charAccess.controlTakeOff.Jump();
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
